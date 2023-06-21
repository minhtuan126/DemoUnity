using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup;
using TMPro;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using System;

// TODO: prease DO NOT - DO NOT - DO NOT use SingletonPersistent for this component!!!!!
public class MainGameUI : SingletonScene<MainGameUI>
{
    [Header("UserData")]
    [SerializeField]
    private Button _userInfoButon;
    [Header("Battle")]
    [SerializeField]
    private Button _battleButton;

    [Header("Inventory")]
    [SerializeField]
    private Button _inventoryButton;

    [Header("Shop")]
    [SerializeField]
    private Button _shopButton;

    [Header("Pearl")]
    [SerializeField]
    private Button _pearlButton;
    [SerializeField]
    private TextMeshProUGUI _pearlCountText;

    [Header("Leaderboard")]
    [SerializeField]
    private Button _leaderBoardButton;

    [Header("Merge Fish")]
    [SerializeField]
    private Button _mergeFishButton;

    [Header("Setting")]
    [SerializeField]
    private Button _settingButton;

    [Header("Egg Piece")]
    [SerializeField]
    private Button _eggPieceButton;
    [SerializeField]
    private TextMeshProUGUI _eggPieceCountText;

    [SerializeField]
    private TextMeshProUGUI _noFishText;

    [SerializeField]
    private TextMeshProUGUI _numberFoodFeed;

    [Header("Feed1")]
    [SerializeField]
    private Button _feedFish1Button;

    [Header("Feed2")]
    [SerializeField]
    private Button _feedFish2Button;

    [Header("SpawnFoodProsition")]
    [SerializeField]
    GameObject _food;
    //
    [SerializeField] private FishSelection fishSelection;
    [SerializeField] private Slider sldRemaining;

    [SerializeField] private GameObject toolAll;
    [SerializeField] private GameObject toolFull;
    [SerializeField] private RectTransform toolFullTargetTransform;
    [SerializeField] private RectTransform toolAllTargetTransform;
    //
    [SerializeField] private TextMeshProUGUI _txtRemaining;

    public static UnityAction<FishInstance> OnChangeFish;
    private FishInstance _currentFish;

    private bool _isFull;
    private bool _needToReset = false;
    private DateTime nextTimeReset;
    private bool isTweening = false;
    private bool isOpenTool = true;
    private List<LTDescr> listTween;
    private DateTime lastTimeUpdateResource;

    // Start is called before the first frame update
    private void Start()
    {
        this.lastTimeUpdateResource = DateTime.Now;
        MainGameUI.OnChangeFish += this.OnChangedFish;
        _userInfoButon.onClick.AddListener(OnPressUserInfo);
        _battleButton.onClick.AddListener(OnPressBattle);
        _inventoryButton.onClick.AddListener(OnInventoryButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _pearlButton.onClick.AddListener(OnPearlButton);
        _mergeFishButton.onClick.AddListener(OnMergeFishButton);
        _settingButton.onClick.AddListener(OnSettingButton);
        _eggPieceButton.onClick.AddListener(OnPearlElementButton);
        _feedFish1Button.onClick.AddListener(OnFeed1Button);
        _feedFish2Button.onClick.AddListener(PlayAnimationTool);
        _leaderBoardButton.onClick.AddListener(OnPressLeaderBoard);
        toolAll.GetComponent<Button>().onClick.AddListener(OnClickToolAll);
        toolFull.GetComponent<Button>().onClick.AddListener(OnClickToolFull);

        EventManager.AddListener(EResourceEvent.UpdateEggPieces, () => this.UpdateTextEggPieces());
        EventManager.AddListener(EResourceEvent.UpdatePearl, () => this.UpdateTextPearl());
        EventManager.AddListener(EResourceEvent.UpdateAllResource, () => { this.UpdateTextPearlAndEggPieces(); });
        EventManager.AddListener<ResourceStruct>(EResourceEvent.UpdateResource, this.OnUpdateResourceSchedule);

        FieldUserData();
        _numberFoodFeed.SetText("-" + GameData.Instance.gameConfig.food_price);
        DateTime now = DateTime.Now.ToUniversalTime();
        this.nextTimeReset = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(1);

    }


    void OnDisable()
    {
        MainGameUI.OnChangeFish -= this.OnChangedFish;
    }

    void OnEnable()
    {
        this.CheckActiveNoFishText();
    }

    private void OnPressUserInfo()
    {
        PopupManager.Instance.OnShowPopup(PopupType.UserInfo);
    }
    public void FieldUserData()
    {
        var userData = GameManager.UserData;
        _pearlCountText.SetText(userData.energy.ToString());
        _eggPieceCountText.SetText(userData.numberEggPieces.ToString());

    }

    private void OnDestroy()
    {
        // _battleButton.onClick -= OnPressBattle();
    }

    public void OnPressBattle()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
        {
            title = "BATTLE",
            status = "COMING SOON",
            confirmText = "OK",
            cancelText = "BACK"
        }, true, ESoundID.ButtonBattle);
    }
    private void OnInventoryButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Inventory);
    }
    private void OnShopButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
        {
            title = "NOTICE",
            status = "Are you sure you want to go to Marketplace?",
            confirmText = "OK",
            cancelText = "CANCEL",
            confirmPopup = () => Extensions.EnterMarketPlace(),
        }, false);
    }
    private void OnPearlButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
        {
            title = "NOTICE",
            status = "Are you sure you want to go to Marketplace?",
            confirmText = "OK",
            cancelText = "CANCEL",
            confirmPopup = () => Extensions.EnterMarketPlace(),
        }, false);
    }
    private void OnMergeFishButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.MergeFish);
    }

    private void OnPressLeaderBoard()
    {
        PopupManager.Instance.OnShowPopup(PopupType.LeaderBoard);
    }

    private void OnSettingButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Setting, new SettingPopupData()
        {
            confirmPopup = () => { },
        });
    }
    private void OnPearlElementButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.ExchangeEgg, new ExchangeEggData()
        {
            // Fake
            eggPieceCost = GameData.Instance.gameConfig.egg_pieces_to_egg
        });
    }
    private void SpawnFood()
    {
        var food = Instantiate<Food>(_food.GetComponent<Food>(), new Vector3(-5, 8, 10), Quaternion.identity);
        food.MoveFood(this._currentFish.mouthPosition.position);
    }
    private void SpawnFoodBig()
    {
        var food = Instantiate<Food>(_food.GetComponent<Food>(), new Vector3(-5, 8, 10), Quaternion.identity);
        food.transform.localScale = new Vector3(2, 2, 2);
        food.MoveFood(this._currentFish.mouthPosition.position);
    }
    private void OnFeed1Button()
    {
        var avaliable = GameManager.UserData.GetAvailableFeedTimeOfUser();
        var isMaxFeed = this.CheckIsMaxEggPieces();
        if (isMaxFeed) return;
        if (this._currentFish.Model.count_eat != 0 && avaliable > 0)
        {
            this.RequestFeedFish().Forget();
        }
        else if (this._currentFish.Model.count_eat == 0)
        {
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "This fish was full.",
                confirmText = "OK",
            }, false);
        }
        else if (avaliable <= 0)
        {
            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "You are not enough Pearl to buy food. Do you want to go to Marketplace?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => Extensions.EnterMarketPlace(),
            }, false);
        }
    }

    private async UniTask RequestFeedFish()
    {
        var response = await APIManager.Instance.FeedFish(this._currentFish.Model);
        if (response)
        {
            this.SpawnFood();
            this.UpdateRemainingBar();
            this.OnRequestFeedComplete(true);
            _currentFish.Feed();
        }
        else
        {
            this.OnRequestFeedComplete(false);
        }
    }

    private void OnClickToolAll()
    {

        var avalible = GameManager.UserData.GetAvailableFeedTimeOfUser();
        var remaining = GameManager.UserData.GetTotalRemainingFeedTimes();
        var feedTime = Mathf.Min(avalible, remaining);
        var isMaxFeed = this.CheckIsMaxEggPieces();
        if (isMaxFeed) return;
        if (avalible > 0)
        {

            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "Do you want to feed all? If yes, all fish would be fed.",
                confirmText = "YES",
                cancelText = "NO",
                confirmPopup = () => this.RequestFeedAll().Forget(),
            }, false);
        }
        else
        {
            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "Not enough Pearl to feed. Go to Marketplace?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => Extensions.EnterMarketPlace(),
            }, false);
        }



    }

    private void OnClickToolFull()
    {
        var avalible = GameManager.UserData.GetAvailableFeedTimeOfUser();
        var remaining = GameManager.UserData.GetRemainingFeedTimesOfish(fishSelection.CurrentFish.Model);
        var feedTime = Mathf.Min(avalible, remaining);

        var isMaxFeed = this.CheckIsMaxEggPieces();
        if (isMaxFeed) return;

        if (avalible > 0 && remaining != 0)
        {

            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "Do you want to feed full?",
                confirmText = "YES",
                cancelText = "NO",
                confirmPopup = () => this.RequestFeedFull().Forget(),
            }, false);
        }
        else if (remaining == 0)
        {
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "This fish was full.",
                confirmText = "OK",
            }, false);
        }
        else
        {
            PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
            {
                title = "NOTICE",
                status = "Not enough Pearl to feed. Go to Marketplace?",
                confirmText = "OK",
                cancelText = "CANCEL",
                confirmPopup = () => Extensions.EnterMarketPlace(),
            }, false);
        }
    }

    private async UniTask RequestFeedFull()
    {
        try
        {
            var response = await APIManager.Instance.FeedFishFull(this._currentFish.Model);
            if (response)
            {
                this.SpawnFoodBig();
                _currentFish.Feed();
                this.UpdateRemainingBar();
                this.OnRequestFeedComplete(true);
            }
            else
            {
                this.OnRequestFeedComplete(false);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private async UniTask RequestFeedAll()
    {
        try
        {
            var listTokenID = GameManager.UserData.GetListFishToFeedAll();
            if (listTokenID.Count != 0)
            {
                var response = await APIManager.Instance.FeedFishAll(listTokenID);
                if (response)
                {
                    this.SpawnFoodBig();
                    _currentFish.Feed();
                    this.UpdateRemainingBar();
                    this.OnRequestFeedComplete(true);
                }
                else
                {
                    this.OnRequestFeedComplete(false);
                }
            }
            else
            {
                PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
                {
                    title = "NOTICE",
                    status = "All of your fishes are full!",
                    confirmText = "OK"
                }, true);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }


    }


    // private void OnFeed2Button()
    // {
    //     this.PlayAnimationTool();
    // }



    private void PlayAnimationTool()
    {
        this.isTweening = true;
        if (this.isOpenTool)
        {
            this.OpenTool();
        }
        else
        {
            this.HideTool();
        }


        // this.isTweening = true;
    }

    private void OpenTool()
    {
        if (this.isTweening)
        {
            LeanTween.cancel(this.toolAll);
            LeanTween.cancel(this.toolFull);
        }
        this.isOpenTool = false;
        LeanTween.move(this.toolAll, this.toolAllTargetTransform.position, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() => this.isTweening = false);

        LeanTween.move(this.toolFull, this.toolFullTargetTransform.position, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() => this.isTweening = false);

    }

    private void HideTool()
    {
        if (this.isTweening)
        {
            LeanTween.cancel(this.toolAll);
            LeanTween.cancel(this.toolFull);
        }
        this.isOpenTool = true;
        var targetOriginal = this._feedFish2Button.GetComponent<RectTransform>().position;
        LeanTween.move(this.toolAll, targetOriginal, 0.5f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() => this.isTweening = false);

        LeanTween.move(this.toolFull, targetOriginal, 0.5f)
            .setOnComplete(() => this.isTweening = false);

    }

    private void OnChangedFish(FishInstance currentFish)
    {
        this._currentFish = currentFish;
        // Debug.Log(JsonUtility.ToJson(this._currentFish.Model));
        this.UpdateRemainingBar();
    }

    private void SetValueRemainingBar(float value)
    {
        this.sldRemaining.value = value;
    }

    private void UpdateRemainingBar()
    {
        if (this._currentFish.Model.count_eat == 0)
        {
            this._isFull = true;
            this.SetValueRemainingBar(1);
        }
        else if (this._currentFish.Model.count_eat < 0)
        {
            this.SetValueRemainingBar(1);
            this._txtRemaining.text = $"∞";
        }
        else
        {
            this._isFull = false;
            var limitFeed = GameData.Instance.GetLimitFeedOfFish(this._currentFish.Model);
            float sldValue = 1 - (float)(this._currentFish.Model.count_eat) / (float)(limitFeed);
            float txtValue = (float)(limitFeed - (float)this._currentFish.Model.count_eat);
            this._txtRemaining.text = $"{txtValue.ToString()} / {limitFeed}";
            this.SetValueRemainingBar(sldValue);
        }
    }

    public FishInstance getCurrentFish()
    {
        return _currentFish;
    }

    void Update()
    {
        if (this._needToReset) return;
        DateTime now = DateTime.Now.ToUniversalTime();
        TimeSpan remainingSecond = this.nextTimeReset.Subtract(now);

        if (remainingSecond.TotalSeconds <= 0)
        {
            this._needToReset = true;
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "It's time to reset fish. Please reload page!",
                confirmText = "Reload",
                confirmPopup = () => Extensions.LogOut(),
            }, true);
            return;
        }

        if (this._isFull && this._currentFish.Model.gen != Gen.F15)
        {
            this._txtRemaining.text = "Reset in " + Extension.ConvertSecondThanMore(remainingSecond.TotalSeconds);
        }
        if (DateTime.Now.Subtract(this.lastTimeUpdateResource).TotalSeconds >= 60)
        {
            this.lastTimeUpdateResource = DateTime.Now;
            try
            {
                APIManager.Instance.UpdateResourceUser().Forget();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

        }
    }

    public void CheckActiveNoFishText()
    {
        if (this._noFishText != null)
            this._noFishText!.gameObject.SetActive(GameManager.UserData.fishList.Count > 0);
    }

    public void OnRequestFeedComplete(bool isSuccess)
    {
        if (isSuccess)
        {
            EventManager.Invoke(EResourceEvent.UpdatePearl);
        }
        else
        {
            Debug.LogWarning("Request Feed Failed");
        }
    }

    public void UpdateTextPearl()
    {
        Extension.TweenTMP(this._pearlCountText, GameManager.oldPearl, GameManager.UserData.energy, 2);
    }

    public void UpdateTextEggPieces()
    {
        Extension.TweenTMP(this._eggPieceCountText, GameManager.oldEggPiece, GameManager.UserData.numberEggPieces, 2);
    }

    public void UpdateTextPearlAndEggPieces()
    {
        this.UpdateTextPearl();
        this.UpdateTextEggPieces();
    }

    public void OnUpdateResourceSchedule(ResourceStruct resource)
    {
        GameManager.oldPearl = resource.energy;
        GameManager.oldEggPiece = resource.numberEggPieces;
        GameManager.UserData.numberEggPieces = resource.numberEggPieces;
        GameManager.UserData.energy = resource.energy;
        Extension.TweenTMP(this._pearlCountText, GameManager.oldPearl, resource.energy, 1);
        Extension.TweenTMP(this._eggPieceCountText, GameManager.oldEggPiece, resource.numberEggPieces, 2);
    }

    public bool CheckIsMaxEggPieces()
    {
        bool isMaxEggPieces = GameData.Instance.CheckIsMaxEggPiecesOfFish(this._currentFish.Model);
        if (isMaxEggPieces)
        {
            PopupManager.Instance.OnShowPopup(PopupType.Notice, new NoticeData()
            {
                title = "NOTICE",
                status = "Your fish has reached the maximum number of egg fragments received in its life cycle. Use this fish to Fusion and create new fish !",
                confirmText = "OK"
            }, true);
        }
        return isMaxEggPieces;
    }


}
