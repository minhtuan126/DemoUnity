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
    public class OpennedNewEggPopup : PopupBase
    {

        [SerializeField]
        private Image _eggAvatar;

        [SerializeField]
        private TextMeshProUGUI _tierText;

        [SerializeField]
        private TextMeshProUGUI _tokenIDText;
        [SerializeField]
        private Button _goInventoryButton;
        [SerializeField]
        private Button _openEggButton;
        [SerializeField]
        private TextMeshProUGUI _openFText;

        public FishModel EggModel { get; private set; }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            EggModel = GetPopupInputData<NewEggPopupData>().model;
        }

        protected override void Start()
        {
            base.Start();
            _goInventoryButton.onClick.AddListener(OnGoInventory);
            _openEggButton.onClick.AddListener(OnOpenEgg);
        }

        protected override void FieldData()
        {
            _eggAvatar.sprite = EggModel.avatar;
            this._tokenIDText.SetText($"#{this.EggModel.tokenId}");
            // _tierText.SetText(EggModel.rarity.ToString());
            // _openFText.SetText(EggModel.gen.ToString());
        }

        private void OnGoInventory()
        {
            OnHide();
            PopupManager.Instance.OnShowPopup(PopupType.Inventory, new InventoryPopupData()
            {
                selectedEgg = EggModel
            });
        }

        private void OnOpenEgg()
        {
            PopupManager.Instance.OnShowPopup(PopupType.OpenEgg, new NewEggPopupData()
            {
                model = EggModel
            });
        }

    }


    public class NewEggPopupData : PopupInputData
    {
        public FishModel model;
    }
}
