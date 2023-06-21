using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Popup
{
    public enum PopupType
    {
        Confirm,
        Setting,
        Inventory,
        ExchangeEgg,
        OpennedNewEgg,
        OpennedNewFish,
        OpenEgg,
        UserInfo,
        MergeFish,
        Loading,
        ConfirmFusion,
        Evolution,
        APIFail,
        Notice,
        FusionFail,
        ConfirmExchangeEgg,
        LeaderBoard,
        Count,
    }

    public class PopupManager : SingletonPersistent<PopupManager>
    {

        [Serializable]
        public struct PopupData
        {

            [ReadOnly]
            public PopupType popupType;
            public PopupBase popupPrefab;
        }

        [SerializeField]
        private PopupData[] _popupPrefabs = new PopupData[(int)PopupType.Count];
        [SerializeField]
        private CanvasGroup _fader;

        private Dictionary<PopupType, PopupBase> _popupPrefabDicts = new Dictionary<PopupType, PopupBase>();
        private Dictionary<PopupType, PopupBase> _popupInstanceDicts = new Dictionary<PopupType, PopupBase>();

        public List<PopupBase> CurrentPopupsOpening { get; private set; } = new List<PopupBase>();

#if UNITY_EDITOR
        private void OnValidate()
        {
            Array.Resize(ref _popupPrefabs, (int)PopupType.Count);

            for (int i = 0; i < _popupPrefabs.Length; i++)
            {
                _popupPrefabs[i].popupType = (PopupType)i;
            }
        }
#endif

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < _popupPrefabs.Length; i++)
            {
                _popupPrefabDicts.Add(_popupPrefabs[i].popupType, _popupPrefabs[i].popupPrefab);
                _popupInstanceDicts.Add(_popupPrefabs[i].popupType, null);
            }
        }


        public PopupBase OnShowPopup(PopupType popupType, bool isHidePrevious = true)
        {
            return OnShowPopup(popupType, null, isHidePrevious);
        }

        public PopupBase OnShowPopup(PopupType popupType, PopupInputData data, bool isHidePrevious = true, ESoundID soundID = ESoundID.Notification)
        {
            PopupBase popup = _popupInstanceDicts[popupType];
            if (popup == null)
            {
                popup = GameObject.Instantiate(_popupPrefabDicts[popupType], transform);
                _popupInstanceDicts[popupType] = popup;
            }

            // Hide Previous popup
            if (isHidePrevious)
            {
                while (CurrentPopupsOpening.Count > 0)
                {
                    var popupObj = CurrentPopupsOpening[CurrentPopupsOpening.Count - 1];
                    if (popupObj != null)
                    {
                        popupObj.OnHide();
                    }
                }
                CurrentPopupsOpening.Clear();
            }

            popup.OnShow(data);

            if (!CurrentPopupsOpening.Contains(popup))
                CurrentPopupsOpening.Add(popup);

            _fader.SetAppear(CurrentPopupsOpening.Count > 0);
            SoundManager.Instance.PlaySound(soundID);
            return _popupInstanceDicts[popupType];
        }

        public void OnHidePopup(PopupBase popup)
        {
            if (CurrentPopupsOpening.Count > 0)
            {
                CurrentPopupsOpening.Remove(popup);
            }
            _fader.SetAppear(CurrentPopupsOpening.Count > 0);
        }

        public void OnHidePopup(PopupType type)
        {
            for (int i = CurrentPopupsOpening.Count - 1; i >= 0; i--)
            {
                Debug.Log(i + ":" + CurrentPopupsOpening[i].GetPopupType());
                if (CurrentPopupsOpening[i].GetPopupType() == type)
                {
                    CurrentPopupsOpening[i].OnHide();
                    //_fader.SetAppear(CurrentPopupsOpening.Count > 0);
                }
            }
        }

        public void OnHideAllPopups()
        {
            for (int i = CurrentPopupsOpening.Count - 1; i >= 0; i--)
            {
                CurrentPopupsOpening[i].OnHide();
            }

            CurrentPopupsOpening.Clear();
            _fader.SetAppear(false);
        }
    }
}
