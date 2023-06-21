using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using GameCore.Models;
using Cysharp.Threading.Tasks;

namespace Popup
{
    public class MergeFishPopup : PopupBase
    {
        [Header("Fish 1")]
        [SerializeField]
        private Button _addFish1;

        [SerializeField]
        private Image _fish1BG;
        [SerializeField]
        private TextMeshProUGUI _fish1TierText;

        [SerializeField]
        private TextMeshProUGUI _tokenID1;

        [SerializeField]
        private Image _iconAddFish1;

        [SerializeField]
        private Image _fish1Avatar;

        [SerializeField]
        private Image _fishBG1;

        [SerializeField]
        private Button _btnRemoveFish1;
        [SerializeField]
        private Image _fish1GenBG;
        [SerializeField]
        private TextMeshProUGUI _genFish1;


        [Header("Fish 2")]

        [SerializeField]
        private Button _addFish2;

        [SerializeField]
        private Image _fishBG2;

        [SerializeField]
        private Button _btnRemoveFish2;

        [SerializeField]
        private Image _fish2BG;
        [SerializeField]
        private TextMeshProUGUI _fish2TierText;
        [SerializeField]
        private Image _fish2GenBG;
        [SerializeField]
        private TextMeshProUGUI _genFish2;

        [SerializeField]
        private TextMeshProUGUI _tokenID2;

        [SerializeField]
        private Image _iconAddFish2;

        [SerializeField]
        private Image _fish2Avatar;

        [SerializeField]
        private Button _fusionButton;
        [SerializeField]
        private FishSelectionPage _fishSelectionPage;
        [SerializeField]
        private Button _selectedFishButton;
        private FishModel _fish1;
        private FishModel _fish2;

        [Header("Texts info")]
        [SerializeField] private TextMeshProUGUI _txtCommon;
        [SerializeField] private TextMeshProUGUI _txtGreat;
        [SerializeField] private TextMeshProUGUI _txtRare;
        [SerializeField] private TextMeshProUGUI _txtEpic;
        [SerializeField] private TextMeshProUGUI _txtFee;
        [SerializeField] private TextMeshProUGUI _rating_success;
        [SerializeField] private TextMeshProUGUI _rating_non_success;

        public bool isBothSelected
        {
            get
            {
                return (this._fish1 != null) && (this._fish2 != null);
            }
        }
        public FishSelectionElement FishInventoryElementSelected1 { get; private set; }
        public FishSelectionElement FishInventoryElementSelected2 { get; private set; }

        private bool isSelectFish1 = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _fusionButton.onClick.AddListener(OnPressedFusionButton);
            _selectedFishButton.onClick.AddListener(OnFinishedSelectFish);
            _addFish1.onClick.AddListener(() => OnShowSelectFish(true));
            _addFish2.onClick.AddListener(() => OnShowSelectFish(false));
            _fusionButton.interactable = false;
            _rating_success.SetText(GameData.Instance.gameConfig.rate_success + "%");
            _rating_non_success.SetText((100 -GameData.Instance.gameConfig.rate_success) + "%");
        }

        private void OnShowSelectFish(bool isFish1)
        {
            isSelectFish1 = isFish1;
            if (isSelectFish1)
            {
                _fishSelectionPage.OnShow(_fish2, _fish1);
            }

            if (!isSelectFish1)
            {
                _fishSelectionPage.OnShow(_fish1, _fish2);
            }
        }

        private void OnFinishedSelectFish()
        {
            var fishSelected = _fishSelectionPage.FishInventoryElementSelected;
            if (fishSelected != null)
            {
                if (isSelectFish1)
                {
                    _fish1 = fishSelected.OwnerFishModel;
                    FishInventoryElementSelected1 = fishSelected;
                    _fish1Avatar.sprite = _fish1.avatar;
                    _fish1Avatar.enabled = true;
                    _iconAddFish1.enabled = false;
                    _fish1TierText.SetText(_fish1.rarity.ToString());
                    this._fish1BG.gameObject.SetActive(true);
                    _fish1BG.sprite = DataReferece.Instance.GetBGTextRarity(_fish1.rarity);
                    _fishBG1.sprite = DataReferece.Instance.GetSpriteRarity(_fish1.rarity);
                    _genFish1.SetText(_fish1.gen.ToString());
                    this._fish1GenBG.gameObject.SetActive(true);
                    _btnRemoveFish1.SetActive(_fish1 != null);
                    this._tokenID1.gameObject.SetActive(true);
                    this._tokenID1.SetText($"#{this._fish1.tokenId}");
                }
                else
                {
                    _fish2 = fishSelected.OwnerFishModel;
                    FishInventoryElementSelected2 = fishSelected;
                    _fish2Avatar.sprite = _fish2.avatar;
                    _fish2Avatar.enabled = true;
                    _iconAddFish2.enabled = false;
                    _fish2TierText.SetText(_fish2.rarity.ToString());
                    this._fish2BG.gameObject.SetActive(true);
                    _fish2BG.sprite = DataReferece.Instance.GetBGTextRarity(_fish2.rarity);
                    _fishBG2.sprite = DataReferece.Instance.GetSpriteRarity(_fish2.rarity);
                    _genFish2.SetText(_fish2.gen.ToString());
                    this._fish2GenBG.gameObject.SetActive(true);
                    this._tokenID2.gameObject.SetActive(true);
                    _btnRemoveFish2.SetActive(_fish2 != null);
                    this._tokenID2.SetText($"#{this._fish2.tokenId}");
                }
            }
            _fishSelectionPage.OnHide();
            if (this._fish1 != null && this._fish2 != null)
            {
                this.OnBothFishSelected();
            }
            _fusionButton.interactable = _fish1 != null && _fish2 != null;
        }

        public void UnselectFish1()
        {
            this._fish1BG.gameObject.SetActive(false);
            this._fish1GenBG.gameObject.SetActive(false);
            this._fish1 = null;
            this._fish1BG.sprite = DataReferece.Instance.GetBGTextRarity(Rarity.None);
            this._fishBG1.sprite = DataReferece.Instance.GetSpriteRarity(Rarity.Rare);
            this._fish1Avatar.enabled = false;
            _iconAddFish1.enabled = true;
            _fish1TierText.SetText("");
            _genFish1.SetText("");
            this.ClearRatioText();
            this._tokenID1.gameObject.SetActive(false);
            if (FishInventoryElementSelected1)
                FishInventoryElementSelected1.SetOn(false);
        }

        public void UnselectFish2()
        {
            this._fish2 = null;
            this._fish2BG.gameObject.SetActive(false);
            this._fish2GenBG.gameObject.SetActive(false);
            this._fish2BG.sprite = DataReferece.Instance.GetBGTextRarity(Rarity.None);
            this._fishBG2.sprite = DataReferece.Instance.GetSpriteRarity(Rarity.Rare);
            this._fish2Avatar.enabled = false;
            _iconAddFish2.enabled = true;
            _fish2TierText.SetText("");
            _genFish2.SetText("");
            this.ClearRatioText();
            this._tokenID2.gameObject.SetActive(false);
            if (FishInventoryElementSelected2)
                FishInventoryElementSelected2.SetOn(false);
        }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            ClearRatioText();
            UnselectFish1();
            UnselectFish2();
            var data = GetPopupInputData<ConfirmPopupData>();
        }

        public void OnPressedFusionButton()
        {
            if (_fish1 == null || _fish2 == null) return;
            PopupManager.Instance.OnShowPopup(PopupType.ConfirmFusion, new ConfirmFusionData()
            {
                title = "FUSION",
                status = "Are You Sure?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => OnConfirmMerged().Forget(),
                fish1 = _fish1,
                fish2 = _fish2,
            }, false);
        }

        private async UniTask OnConfirmMerged()
        {
            try
            {
                // fale
                var loadingPopup = PopupManager.Instance.OnShowPopup(PopupType.Loading, false);
                var dataMerge = await APIManager.Instance.BreedFish(GameManager.UserData.address, _fish1.tokenId.ToString(), _fish2.tokenId.ToString());
                EventManager.Invoke(EResourceEvent.UpdatePearl);
                if (dataMerge != null && dataMerge.Count > 2)
                {
                    var result = await ContractMgr.Instance.BreedFish(dataMerge[0], dataMerge[1], dataMerge[3], dataMerge[2], dataMerge[4], dataMerge[5], dataMerge[6]);
                    await UniTask.Delay(10000);
                    if (result)
                    {
                        var lastTokenId = await ContractMgr.Instance.GetLastFish();
                        if (lastTokenId == null)
                        {
                            PopupManager.Instance.OnHideAllPopups();
                            await UniTask.Delay(500);
                            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                            {
                                title = "NOTICE",
                                status = "Cannot get information from BSC server!",
                                confirmText = "RELOAD",
                                confirmPopup = () => Extensions.LogOut(),
                            }, true);
                            return;
                        }
                        else
                        {
                            var newFish = await APIManager.Instance.ReloadFish(lastTokenId);
                            newFish.count_eat = 10;
                            EventManager.Invoke(FishEvent.RemoveFishInstance, _fish1.tokenId);
                            EventManager.Invoke(FishEvent.RemoveFishInstance, _fish2.tokenId);
                            EventManager.Invoke(FishEvent.SpawnNewFishInstance, newFish, false);
                            GameManager.UserData.RemoveFish(_fish1.tokenId);
                            GameManager.UserData.RemoveFish(_fish2.tokenId);
                            GameManager.UserData.AddNewFish(newFish);
                            PopupManager.Instance.OnHideAllPopups();
                            if ((int)newFish.gen == (int)Gen.F4 || (int)newFish.gen == (int)Gen.F8 || (int)newFish.gen == (int)Gen.F12)
                            {
                                PopupManager.Instance.OnShowPopup(PopupType.Evolution, new OpenNewFishPopupData()
                                {
                                    model = newFish
                                }, true);
                            }
                            else
                            {
                                PopupManager.Instance.OnShowPopup(PopupType.OpennedNewFish, new OpenNewFishPopupData()
                                {
                                    model = newFish
                                }, true);
                            }

                            this.UnselectFish1();
                            this.UnselectFish2();
                        }
                        return;
                    }
                }

                PopupManager.Instance.OnHideAllPopups();
                if (dataMerge != null && dataMerge.Count > 0 && dataMerge[0] == "money")
                {
                    PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                    {
                        title = "NOTICE",
                        status = "You are not enough Pearls to fusion.",
                        confirmText = "OK"
                    }, true);
                }
                else
                {
                    PopupManager.Instance.OnShowPopup(PopupType.FusionFail, new FusionFailData()
                    {
                        fish1 = this._fish1,
                        fish2 = this._fish2
                    }, true);
                }
            }
            catch (System.Exception e)
            {
                PopupManager.Instance.OnHideAllPopups();
                Debug.LogError("Error Merge Fish " + e.ToString());
                throw;
            }
        }

        public async UniTask<RatioBreed> RequestGetSuccessRatio()
        {
            var ratio = await APIManager.Instance.GetRateBreedFish((int)this._fish1.tokenId, (int)this._fish2.tokenId);
            if (ratio != null)
            {
                this._txtCommon.SetText($"{ratio.common}%");
                this._txtGreat.SetText($"{ratio.great}%");
                this._txtRare.SetText($"{ratio.rare}%");
                this._txtEpic.SetText($"{ratio.epic}%");
                this._txtFee.SetText($"{ratio.fee}");
            }
            await UniTask.Delay(2000);
            //PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnHidePopup(PopupType.Loading);
            return null;
        }

        public void OnBothFishSelected()
        {
            PopupManager.Instance.OnShowPopup(PopupType.Loading, false);
            this.RequestGetSuccessRatio().Forget();
        }

        public void ClearRatioText()
        {
            this._txtCommon.SetText("0%");
            this._txtEpic.SetText("0%");
            this._txtGreat.SetText("0%");
            this._txtRare.SetText("0%");
            this._txtFee.SetText("0");
        }

        public override void OnHide()
        {
            base.OnHide();
            _fishSelectionPage.OnHide();
        }
    }
}
