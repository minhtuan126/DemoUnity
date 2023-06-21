using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Popup
{
    public class ConfirmPopup : PopupBase
    {

        [SerializeField]
        private TextMeshProUGUI _titleText;

        [SerializeField]
        private TextMeshProUGUI _statusText;

        [SerializeField]
        private ButtonData _confirmButton;

        [SerializeField]
        private ButtonData _cancelButton;

        private Action _confirmPopup;
        private Action _cancelPopup;

        // Start is called before the first frame update
        protected override void Start()
        {
            if (_cancelButton.button)
                _cancelButton.button.onClick.AddListener(OnCancel);
            if (_confirmButton.button)
                _confirmButton.button.onClick.AddListener(OnConfirm);
        }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);

            var data = GetPopupInputData<ConfirmPopupData>();
            if (_titleText)
                _titleText.SetText(data.title);
            if (_statusText)
                _statusText.SetText(data.status);
            if (_confirmButton.buttonText)
                _confirmButton.buttonText.SetText(data.confirmText);
            if (_cancelButton.buttonText)
                _cancelButton.buttonText.SetText(data.cancelText);

            _confirmPopup = data.confirmPopup;
            _cancelPopup = data.cancelPopup;
        }

        private void OnConfirm()
        {
            OnHide();
            if (_confirmPopup != null) _confirmPopup.Invoke();

        }
        private void OnCancel()
        {
            OnHide();
            if (_cancelPopup != null) _cancelPopup.Invoke();
        }


        [System.Serializable]
        public struct ButtonData
        {
            public Button button;
            public TextMeshProUGUI buttonText;
        }
    }

    public class ConfirmPopupData : PopupInputData
    {
        public string title;
        public string status;
        public string confirmText;
        public string cancelText;

        public Action confirmPopup;
        public Action cancelPopup;
    }
}
