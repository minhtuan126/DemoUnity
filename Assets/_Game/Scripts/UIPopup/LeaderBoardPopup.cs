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
    public class LeaderBoardPopup : PopupBase
    {
        [Header("Fish")]
        [SerializeField]
        private Toggle _fishToggle;
        [SerializeField]
        private GameObject _fishPage;
        [SerializeField]
        private ToggleGroup _fishToggleGroup;
        [SerializeField]
        private RectTransform fishPageContent;

        [Header("PVP")]
        [SerializeField]
        private Toggle _pvpToggle;
        [SerializeField]
        private GameObject _pvpPage;
        [SerializeField]
        private ToggleGroup _pvpToggleGroup;
        [SerializeField]
        private RectTransform pvpPageContent;

        [Header("Element")]
        [SerializeField]
        private LeaderBoardElement _leaderBoardPrefab;
        private List<LeaderBoardElement> _leaderBoardElements = new List<LeaderBoardElement>();

        private List<UserData> listuser;
        public enum InventoryPage
        {
            Fish,
            Egg
        }



        protected override void Start()
        {
            _fishToggle.onValueChanged.AddListener(OnFishPageToggleChange);
            _pvpToggle.onValueChanged.AddListener(OnPVPToggleChange);
            base.Start();
        }

        private void OnFishPageToggleChange(bool isEnable)
        {
            Debug.Log(string.Format("On FishPage: {0}", isEnable ? "On" : "Off"));
            if (isEnable)
            {
                _fishToggle.transform.SetAsLastSibling();
                _fishPage.gameObject.SetActive(isEnable);
                _pvpPage.gameObject.SetActive(!isEnable);
            }
        }


        private void OnPVPToggleChange(bool isEnable)
        {
            if (isEnable)
            {
                _pvpToggle.transform.SetAsLastSibling();
                _fishPage.gameObject.SetActive(!isEnable);
                _pvpPage.gameObject.SetActive(isEnable);
            }
        }

        public async override void OnShow(PopupInputData popupData)
        {
            foreach(Transform child in fishPageContent.transform)
                {
                    Destroy(child.gameObject);
                }
            var data = popupData as LeaderboardPopupData;
            listuser = await APIManager.Instance.GetLeaderboard();
            var rank = 1;
            foreach (var user in listuser) 
            {
                _leaderBoardPrefab._rankText.SetText(rank.ToString());
                var newElement = Instantiate(_leaderBoardPrefab,fishPageContent);
                newElement.Init(user,_fishToggleGroup);
                _leaderBoardElements.Add(newElement);
                rank++;
            }
            this.fishPageContent.SetActive(true);
            base.OnShow(popupData);
        }

        public override void OnHide()
        {
            base.OnHide();
        }
    }

    public class LeaderboardPopupData : PopupInputData
    {
        public List<UserData> users;
    }
}

