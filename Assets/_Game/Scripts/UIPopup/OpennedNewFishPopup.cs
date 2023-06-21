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
    public class OpennedNewFishPopup : PopupBase
    {

        [SerializeField]
        private Image _fishAvatar;

        [SerializeField]
        private TextMeshProUGUI _tierText;

        [SerializeField]
        private TextMeshProUGUI _genText;

        [SerializeField]
        private TextMeshProUGUI _tokenIDText;

        [SerializeField]
        private Image _tierBG;

        [SerializeField]
        private Button _goInventoryButton;

        public FishModel FishModel { get; private set; }

        public override void OnShow(PopupInputData popupData)
        {
            FishModel = (popupData as OpenNewFishPopupData).model;
            base.OnShow(popupData);

        }

        protected override void Start()
        {
            base.Start();
            _goInventoryButton.onClick.AddListener(OnGoInventory);
        }

        protected override void FieldData()
        {
            _fishAvatar.sprite = FishModel.avatar;
            _tierText.SetText(FishModel.rarity.ToString());
            this._tokenIDText.SetText($"#{this.FishModel.tokenId}");
            _tierBG.sprite = DataReferece.Instance.GetBGTextRarity(FishModel.rarity);
            this._genText.text = this.FishModel.gen.ToString();
        }

        private void OnGoInventory()
        {
            PopupManager.Instance.OnShowPopup(PopupType.Inventory, new InventoryPopupData()
            {
                selectedFish = FishModel
            });
        }
    }


    public class OpenNewFishPopupData : PopupInputData
    {
        public FishModel model;
    }
}
