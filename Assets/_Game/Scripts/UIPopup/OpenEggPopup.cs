using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Timers;
using TMPro;
using GameCore.Models;
namespace Popup
{
    public class OpenEggPopup : PopupBase
    {

        [SerializeField]
        private Image _eggAvatar;
        [SerializeField]
        private Image _eggAnimAvatar;

        [SerializeField]
        private Image _eggbackground;

        [SerializeField]
        private Button _openButton;

        [SerializeField]
        private GameObject _openEggAnim;

        [Header("Tier Texts")]

        [SerializeField]
        private TextMeshProUGUI _commonText;
        [SerializeField]
        private TextMeshProUGUI _greatText;
        [SerializeField]
        private TextMeshProUGUI _rareText;
        [SerializeField]
        private TextMeshProUGUI _epicText;

        public FishModel EggModel { get; private set; }

        public override void OnShow(PopupInputData popupData)
        {
            EggModel = (popupData as NewEggPopupData).model;
            base.OnShow(popupData);
            this.FieldData();
        }

        protected override void Start()
        {
            base.Start();
            _openButton.onClick.AddListener(OnOpenEgg);
        }

        protected override void FieldData()
        {
            _eggAvatar.sprite = EggModel.avatar;
            _eggAnimAvatar.sprite = EggModel.avatar;
            // TODO: set color
            // _eggbackground.SetText(EggModel.fishTier.ToString());
        }

        private void OnOpenEgg()
        {
            PlayAnimOpenEgg().Forget();
        }

        private async UniTask PlayAnimOpenEgg()
        {
            _openEggAnim.SetActive(true);
            var a = SoundManager.Instance.PlaySound(ESoundID.WaitEgg,true);
            var loadingpopup = PopupManager.Instance.OnShowPopup(PopupType.Loading, false);
            List<string> dataHatch = await APIManager.Instance.HatchEgg(GameManager.UserData.address, EggModel.tokenId.ToString());

            bool success = await ContractMgr.Instance.HatchFish(dataHatch[0], dataHatch[1], dataHatch[3], dataHatch[4], dataHatch[5]);
            // await UniTask.Delay(5000); 
            await UniTask.Delay(10000);   
            SoundManager.Instance.Repool(a);
            if (success)
            {
                FishModel newFish = await APIManager.Instance.ReloadFish(dataHatch[0]);
                int countReload = 0;
                while (newFish.gen == Gen.Egg) {
                    newFish = await APIManager.Instance.ReloadFish(dataHatch[0]);
                    countReload++;
                    await UniTask.Delay(2000);
                    if (countReload == 5)
                    {
                        break;
                    }
                }
                if (newFish.gen == Gen.Egg) {
                    PopupManager.Instance.OnHideAllPopups();
                    PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                    {
                        title = "NOTICE",
                        status = "Cannot get information from BSC server!",
                        confirmText = "RELOAD",
                        confirmPopup = () => Extensions.LogOut(),
                    }, true);
                    return;
                }
                // TODO: Hard code, set fish to F0, It should be set from API
                newFish.gen = Gen.F0;
                GameManager.UserData.RemoveEgg(EggModel.tokenId);
                GameManager.UserData.AddNewFish(newFish);
                EventManager.Invoke(FishEvent.SpawnNewFishInstance, newFish, false);
                _openEggAnim.SetActive(false);
                PopupManager.Instance.OnHideAllPopups();
                PopupManager.Instance.OnShowPopup(PopupType.OpennedNewFish, new OpenNewFishPopupData()
                {
                    model = newFish
                }, true, ESoundID.HatchEgg);
            }
            else
            {
                _openEggAnim.SetActive(false);
                PopupManager.Instance.OnHideAllPopups();
                PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                {
                    title = "NOTICE",
                    status = "Open Egg Fail!!",
                    confirmText = "OK"
                }, true, ESoundID.Error);
            }
            OnHide();
        }
    }
}
