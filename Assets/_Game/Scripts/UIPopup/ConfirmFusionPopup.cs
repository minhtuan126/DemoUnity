using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using GameCore.Models;

namespace Popup
{
    public class ConfirmFusionPopup : ConfirmPopup
    {
        [SerializeField]
        private Image _fish1Avatar;
        [SerializeField]
        private Image _fish2Avatar;

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            var data = GetPopupInputData<ConfirmFusionData>();
            _fish1Avatar.sprite = data.fish1.avatar;
            _fish2Avatar.sprite = data.fish2.avatar;
        }
    }

    public class ConfirmFusionData : ConfirmPopupData
    {
        public FishModel fish1;
        public FishModel fish2;
    }
}
