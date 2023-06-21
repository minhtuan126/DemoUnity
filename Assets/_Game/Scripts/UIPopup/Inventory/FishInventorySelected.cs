using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using Popup;

public class FishInventorySelected : MonoBehaviour
{
    [SerializeField]
    private Image _fishAvatar;
    [SerializeField] private TextMeshProUGUI _spd;
    [SerializeField] private TextMeshProUGUI _atk;
    [SerializeField] private TextMeshProUGUI _hp;
    [SerializeField] private TextMeshProUGUI _gen;
    [SerializeField] private TextMeshProUGUI _rarity;
    [SerializeField] private TextMeshProUGUI _eggPiecesLimit;
    [SerializeField] private TextMeshProUGUI _eggPiecesPerFeed;
    [SerializeField] private GameObject _unlockSPD;
    [SerializeField] private GameObject _unlockHP;
    [SerializeField] private GameObject _unlockATK;
    [SerializeField] private GameObject _iconEggPiece;
    [SerializeField] private Image _bgRarity;



    [SerializeField]
    private TextMeshProUGUI _token_id;
    [SerializeField]
    private Button _sell;

    public FishModel OwnerFishModel { get; protected set; }

    // Start is called before the first frame update
    private void Start()
    {
        _sell.onClick.AddListener(OnSellButton);
    }
    public void OnSelectedFish(FishInventoryElement selected)
    {
        OwnerFishModel = selected.OwnerFishModel;
        int limitEggPieces = GameData.Instance.GetLimitEggPiecesOfFish(this.OwnerFishModel);
        int epPerFeed = GameData.Instance.GetNumberEggPiecesPerFeedOfFish(this.OwnerFishModel);
        _fishAvatar.sprite = selected.FishAvatar;
        _token_id.text = "ID:" + selected.OwnerFishModel.tokenId;
        this._spd.SetText(this.OwnerFishModel.spd.ToString());
        this._atk.SetText(this.OwnerFishModel.atk.ToString());
        this._hp.SetText(this.OwnerFishModel.hp.ToString());
        this._gen.SetText(this.OwnerFishModel.gen.ToString());
        this._rarity.SetText(this.OwnerFishModel.rarity.ToString());
        this._eggPiecesLimit.SetText(limitEggPieces >= 0 && OwnerFishModel.gen != Gen.F15 ? limitEggPieces.ToString() : "Unlimited");
        this._eggPiecesPerFeed.SetText("+" + epPerFeed);
        this._unlockATK.SetActive(OwnerFishModel.atk == 0);
        this._unlockSPD.SetActive(OwnerFishModel.spd == 0);
        this._unlockHP.SetActive(OwnerFishModel.hp == 0);
        this._iconEggPiece.SetActive(OwnerFishModel.available_egg_pieces > 0 || OwnerFishModel.gen == Gen.F15);
        this._bgRarity.sprite = DataReferece.Instance.GetBGTextRarity(this.OwnerFishModel.rarity);
        EventManager.Invoke<FishModel>(FishEvent.SelectMainFish, selected.OwnerFishModel);
    }

    public void OnSelectedFish(FishModel selected)
    {
        OwnerFishModel = selected;
        _fishAvatar.sprite = selected.avatar;
    }
    public void OnSellButton()
    {
        PopupManager.Instance.OnShowPopup(PopupType.Confirm, new ConfirmPopupData()
        {
            title = "NOTICE",
            status = "Do you want to go to Marketplace?",
            confirmText = "OK",
            cancelText = "CANCEL",
            confirmPopup = () => Extensions.EnterMarketPlace(),
        }, false, ESoundID.ButtonSell);
    }
}
