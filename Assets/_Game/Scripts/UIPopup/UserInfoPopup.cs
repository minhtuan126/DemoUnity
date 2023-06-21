using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text;

namespace Popup
{
    public class UserInfoPopup : PopupBase
    {
        [SerializeField]
        private Image _avatar;

        [SerializeField]
        private TextMeshProUGUI _userID;

        [SerializeField]
        private TextMeshProUGUI _walletID;
        [SerializeField]
        private TextMeshProUGUI _eggCount;
        [SerializeField]
        private TextMeshProUGUI _pearlCount;

        [SerializeField]
        private Button _editUserID;
        [SerializeField]
        private Button _editAvatar;

        [Header("User ID Editor")]
        [SerializeField]
        private CanvasGroup _editUserIDContent;

        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private Button _finishedEditIDButton;
        [SerializeField]
        private Button _cancelEditIDButton;

        [Header("User Avatart Editor")]
        [SerializeField]
        private CanvasGroup _editAvatarContent;

        [SerializeField]
        private AvatarSelected _editAvatarSelected;

        private const int ID_length = 13;

        protected override void Start()
        {
            base.Start();
            _editUserID.onClick.AddListener(OnPressedEditUserID);
            _editAvatar.onClick.AddListener(OnPressedEditAvatar);
            _finishedEditIDButton.onClick.AddListener(() => OnEditedUserID(true));
            _cancelEditIDButton.onClick.AddListener(() => OnEditedUserID(false));
            _editAvatarSelected.OnSelectAvatar.AddListener(OnFinishedEditAvatar);

            _avatar.sprite = GameManager.UserData.avatar;
            _inputField.onValueChanged.AddListener(OnTextChange);
        }

        protected override void FieldData()
        {
            var userData = GameManager.UserData;
            _userID.SetText(userData.name);
            var idString = userData.address;
            if (idString.Length > ID_length)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(idString.Substring(0, 5));
                sb.Append("...");
                sb.Append(idString.Substring(idString.Length - (ID_length - 8)));
                _walletID.SetText(sb.ToString());
            }
            _eggCount.SetText(userData.numberEggPieces.ToString());
            _pearlCount.SetText(userData.energy.ToString());
        }

        private void OnPressedEditUserID()
        {
            _editUserIDContent.SetAppear(true);
            _editAvatarContent.SetAppear(false);
        }

        private void OnPressedEditAvatar()
        {
            _editUserIDContent.SetAppear(false);
            _editAvatarContent.SetAppear(true);
        }


        private void OnFinishedEditAvatar(Sprite sprite)
        {
            _editUserIDContent.SetAppear(false);
            _editAvatarContent.SetAppear(false);

            if (sprite != null)
            {
                GameManager.UserData.avatar = sprite;
                _avatar.sprite = GameManager.UserData.avatar;
                GameManager.UpdateUserInfo();
            }
        }

        public void OnEditedUserID(bool finished)
        {
            _editUserIDContent.SetAppear(false);
            _editAvatarContent.SetAppear(false);
            if (finished)
            {
                GameManager.UserData.name = _inputField.text;
                _userID.SetText(GameManager.UserData.name);
                GameManager.UpdateUserInfo();
            }
        }

        private void OnTextChange(string text)
        {
            string newText = "";
            foreach (char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'z') || (c == ' ' || c == '_' || c == '-'))
                {
                    newText += c;
                }
            }
            _inputField.text = newText;
        }
    }

}
