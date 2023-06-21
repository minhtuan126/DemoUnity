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
    public class EvolutionPopup : PopupBase
    {

        [SerializeField]
        private Image _fishAvatar;

        [SerializeField]
        private TextMeshProUGUI _unlock;

        [SerializeField]
        private Button _btnOK;
        public FishModel FishModel { get; private set; }

        protected override void Start()
        {
            base.Start();
            _btnOK.onClick.AddListener(this.OnClickOK);
        }

        public override void OnShow(PopupInputData popupData)
        {
            FishModel = (popupData as OpenNewFishPopupData).model;
            base.OnShow(popupData);

        }
        

        protected override void FieldData()
        {
            _fishAvatar.sprite = FishModel.avatar;
            switch((int)this.FishModel.gen)
            {
                case (int)Gen.F4 : 
                    this._unlock.SetText("Unlock HP");
                break;

                case (int)Gen.F8 : 
                    this._unlock.SetText("Unlock SPD");
                break;

                case (int)Gen.F12 : 
                    this._unlock.SetText("Unlock ATK");
                break;
            }
        }

        private void OnGoInventory()
        {

        }

        public override void OnHide()
        {
            base.OnHide();
            PopupManager.Instance.OnShowPopup(PopupType.MergeFish);
        }


        private void OnOpenFish()
        {
            OnHide();
        }

        private void OnClickOK()
        {
            PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnShowPopup(PopupType.OpennedNewFish, new OpenNewFishPopupData()
            {
                model = this.FishModel
            }, true);
        //     LeanTween.delayedCall(2,()=>
        //     {PopupManager.Instance.OnShowPopup(PopupType.OpennedNewFish, new OpenNewFishPopupData()
        //     {
        //         model = this.FishModel
        //     }, true);
        //     });
        }
    }
}
