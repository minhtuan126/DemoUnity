using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Timers;
using System;
using GameCore.Models;

namespace Popup
{
    public class InventoryPopup : PopupBase
    {
        [SerializeField]
        private Toggle _fishToggle;
        [SerializeField]
        private Toggle _eggToggle;

        [SerializeField]
        private FishPageInventory _fishPage;
        [SerializeField]
        private EggPageInventoty _eggPage;

        public enum InventoryPage
        {
            Fish,
            Egg
        }

        protected override void Start()
        {
            _fishToggle.onValueChanged.AddListener(OnFishPageToggleChange);
            _eggToggle.onValueChanged.AddListener(OnEggToggleChange);
            base.Start();
        }

        private void OnFishPageToggleChange(bool isEnable)
        {
            if (isEnable)
            {
                _fishToggle.transform.SetAsLastSibling();
                _fishPage.gameObject.SetActive(isEnable);
                _eggPage.gameObject.SetActive(!isEnable);
            }
        }


        private void OnEggToggleChange(bool isEnable)
        {
            if (isEnable)
            {
                _eggToggle.transform.SetAsLastSibling();
                _fishPage.gameObject.SetActive(!isEnable);
                _eggPage.gameObject.SetActive(isEnable);
            }
        }

        public override void OnShow(PopupInputData popupData)
        {
            var data = popupData as InventoryPopupData;
            if (data != null)
            {
                if (data.selectedFish != null)
                {
                    _fishToggle.isOn = true;
                    _fishPage.OnShow(data.selectedFish).Forget();
                    _eggPage.OnShow();
                    _eggPage.SetActive(false);
                    _fishPage.SetActive(true);
                }
                else if (data.selectedEgg != null)
                {
                    _eggToggle.isOn = true;
                    _eggPage.OnShow(data.selectedEgg);
                    _fishPage.OnShow(null, true, false).Forget();
                    _fishPage.SetActive(false);
                    _eggPage.SetActive(true);
                }
            }
            else
            {
                _fishToggle.isOn = true;
                _eggToggle.isOn = false;

                _eggPage.OnShow();
                _fishPage.OnShow(MainGameUI.Instance.getCurrentFish().Model).Forget();
            }

            base.OnShow(popupData);
        }
        public void OnClickExchangeButton()
        {
            PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnShowPopup(PopupType.ExchangeEgg, new ExchangeEggData()
            {
                eggPieceCost = GameData.Instance.gameConfig.egg_pieces_to_egg
            });
        }
        public void OnClickMarketPlaceButton()
        {
            PopupManager.Instance.OnHideAllPopups();
            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "Are you sure you want to go to Marketplace?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => Extensions.EnterMarketPlace(),
            }, true);
        }

        public override void OnHide()
        {
            _eggPage.SetActive(false);
            _fishPage.SetActive(false);
            base.OnHide();
        }
    }

    public class InventoryPopupData : PopupInputData
    {
        public FishModel selectedEgg;
        public FishModel selectedFish;
    }
}

