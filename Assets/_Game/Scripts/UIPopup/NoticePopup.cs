using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Popup
{
    public class NoticePopup : PopupBase
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
            _cancelButton.button.onClick.AddListener(OnCancel);
            _confirmButton.button.onClick.AddListener(OnConfirm);
        }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            
            var data = GetPopupInputData<NoticeData>();
            _titleText.SetText(data.title);
            _statusText.SetText(data.status);
            _confirmButton.buttonText.SetText(data.confirmText);
            _cancelButton.buttonText.SetText(data.cancelText);

            _confirmPopup = data.confirmPopup;
            _cancelPopup = data.cancelPopup;
        }


        // public override void OnHide()
        // {

        // }

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

    public class NoticeData : PopupInputData
    {
        public string title;
        public string status;
        public string confirmText;
        public string cancelText;

        public Action confirmPopup;
        public Action cancelPopup;
    }
}
