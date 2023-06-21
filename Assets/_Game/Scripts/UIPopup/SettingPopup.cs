using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;

namespace Popup
{
    public class SettingPopup : PopupBase
    {
        [SerializeField]
        private Slider _soundSlider;
        [SerializeField]
        private Slider _vfxSlider;
        [SerializeField]
        private Button _aboutButton;

        private Action _confirmPopup;

        // Start is called before the first frame update
        protected override void Start()
        {
            _soundSlider.onValueChanged.AddListener(OnSoundToggleChange);
            _vfxSlider.onValueChanged.AddListener(OnVFXToggleChange);
            _aboutButton.onClick.AddListener(OnAbout);
            base.Start();
        }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            var data = GetPopupInputData<SettingPopupData>();
            _confirmPopup = data.confirmPopup;

            var UserData = GameManager.UserData;
            _soundSlider.value = GameManager.UserData.sound_setting;
            _vfxSlider.value = GameManager.UserData.vfx_setting;
            this._type = PopupType.Setting;
        }

        public override void OnHide()
        {
            if (_confirmPopup != null) _confirmPopup.Invoke();
            base.OnHide();

            APIManager.Instance.UpdateSoundSetting(GameManager.UserData).Forget();
            SoundManager.SaveVolume();
        }

        private void OnSoundToggleChange(float value)
        {
            SoundManager.Instance.SetVolumeBGMusic(value);
            GameManager.UserData.sound_setting = value;
        }

        private void OnVFXToggleChange(float value)
        {
            SoundManager.Instance.SetVolumeSFX(value);
            GameManager.UserData.vfx_setting = value;
        }

        public void OnLogOut()
        {
            Action logoutCallback = ()=>
            {
                Extensions.LogOut();
                // PopupManager.Instance.OnHideAllPopups();
            };

            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "CONFIRMATION",
                status = "Are you sure you want to logout?",
                confirmText = "YES",
                cancelText = "NO",
                confirmPopup = logoutCallback,
            }, false);

        }
        public void OnAbout()
        {
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "CREDIT",
                status = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Quis ipsum suspendisse ultrices gravida. Risus commodo viverra maecenas accumsan lacus vel facilisis. ",
                confirmText = "OK",
            }, false);
        }
    }


    public class SettingPopupData : PopupInputData
    {
        public Action confirmPopup;
    }
}
