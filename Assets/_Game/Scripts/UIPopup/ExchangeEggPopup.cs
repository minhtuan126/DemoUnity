using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Timers;
using TMPro;
using GameCore.Models;
using System;

namespace Popup
{
    public class ExchangeEggPopup : PopupBase
    {

        [SerializeField]
        private TextMeshProUGUI _eggPieceValueText;

        [SerializeField]
        private TextMeshProUGUI _exchangeCostText;

        [SerializeField]
        private Button _exchangeButton;

        [Header("Egg Exchanged")]

        [SerializeField]
        private Image _eggExchangedAvatar;

        public ExchangeEggData ExchangeData { get; private set; }

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            ExchangeData = GetPopupInputData<ExchangeEggData>();
        }

        protected override void Start()
        {
            base.Start();
            _exchangeButton.onClick.AddListener(OnExchangeEgg);
        }

        protected override void FieldData()
        {
            var userData = GameManager.UserData;
            _eggPieceValueText.SetText(userData.numberEggPieces.ToString());
            _exchangeCostText.SetText(ExchangeData.eggPieceCost.ToString());

            // TODO: need get from config or API
            _exchangeButton.interactable = userData.numberEggPieces >= 1000;
        }

        private void OnExchangeEgg()
        {
            PopupManager.Instance.OnShowPopup(PopupType.ConfirmExchangeEgg, new ConfirmPopupData()
            {
                title = "EXCHANGE EGG",
                status = "Are You Sure?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => OnConfirmExchange().Forget(),
            }, false);
        }

        private async UniTask OnConfirmExchange()
        {
            PopupManager.Instance.OnShowPopup(PopupType.Loading, false);
            try {
                var dataExchange = await APIManager.Instance.ExchangeEgg(GameManager.UserData.address);
                if (dataExchange!=null && dataExchange.Count > 0)
                {
                    var result = await ContractMgr.Instance.ExchangEgg(dataExchange[1], dataExchange[0], dataExchange[2], dataExchange[3], dataExchange[4]);
                    await UniTask.Delay(10000);
                    if (result)
                    {
                        var lastTokenId = await ContractMgr.Instance.GetLastFish();
                        if (lastTokenId == null)
                        {
                            PopupManager.Instance.OnHideAllPopups();
                            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                            {
                                title = "NOTICE",
                                status = "Cannot get information from BSC server!",
                                confirmText = "RELOAD",
                                confirmPopup = () => Extensions.LogOut(),
                            }, true);
                            return;
                        }
                        var newEgg = await APIManager.Instance.ReloadFish(lastTokenId);
                        var userInfo = await APIManager.Instance.RequestSuccessExchangeEgg(lastTokenId);
                        if (newEgg != null)
                        {
                            GameManager.UserData.RemoveEgg(newEgg.tokenId);
                            GameManager.UserData.AddNewFish(newEgg);
                            PopupManager.Instance.OnHideAllPopups();
                            PopupManager.Instance.OnShowPopup(PopupType.OpennedNewEgg, new NewEggPopupData()
                            {
                                model = newEgg,
                            }, false);
                        }
                        if (userInfo != null)
                        {
                            // GameManager.UserData = userInfo;
                            GameManager.UserData.numberEggPieces = userInfo.numberEggPieces;
                            GameManager.UserData.energy = userInfo.energy;
                            EventManager.Invoke(EResourceEvent.UpdateAllResource);
                            this._eggPieceValueText.SetText(userInfo.numberEggPieces.ToString());
                        }

                        SoundManager.Instance.PlaySound(ESoundID.ExchangeComplete);
                    }
                    else
                    {
                        PopupManager.Instance.OnHideAllPopups();
                        // popup error
                    }
                } else {
                    await UniTask.Delay(500);
                    PopupManager.Instance.OnHideAllPopups();
                    await UniTask.Delay(500);
                    PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                    {
                        title = "NOTICE",
                        status = "Can not exchange egg!",
                        confirmText = "OK",
                    }, true);
                }
            } catch (Exception ex) {
                await UniTask.Delay(1000);
                PopupManager.Instance.OnHideAllPopups();
                await UniTask.Delay(1000);
                PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                {
                    title = "NOTICE",
                    status = "Cannot get connect to game server!",
                    confirmText = "RELOAD",
                    confirmPopup = () => Extensions.LogOut(),
                }, false);
                return;
            }
        }
    }


    public class ExchangeEggData : PopupInputData
    {
        public int eggPieceCost;
    }
}
