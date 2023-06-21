using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using Popup;

public class EggInventorySelected : MonoBehaviour
{
    [SerializeField]
    private Image _eggAvatar;
    [SerializeField]
    private Button _openEgg;
    [SerializeField]
    private TextMeshProUGUI _token_id;
    [SerializeField]
    private Button _sell;

    public EggInventoryElement EggInventoryElementSelected { get; private set; }

    public FishModel OwnerEggModel { get; protected set; }

    private void Start()
    {
        //_eggAvatar.enabled = false;
        //_token_id.text = "";
        _openEgg.onClick.AddListener(OnOpenEgg);
        _sell.onClick.AddListener(OnSellButton);
    }
    // Start is called before the first frame update
    public void OnSelectedEgg(EggInventoryElement selected)
    {
        EggInventoryElementSelected = selected;
        OwnerEggModel = EggInventoryElementSelected.OwnerEggModel;
        _token_id.text = "ID:" + selected.OwnerEggModel.tokenId;
        switch (OwnerEggModel.category)
        {
            case 0:_eggAvatar.sprite = selected.eggSprites[0];break;
            case 1:_eggAvatar.sprite = selected.eggSprites[1];break;
            case 2:_eggAvatar.sprite = selected.eggSprites[2];break;
            case 3:_eggAvatar.sprite = selected.eggSprites[3];break;
        }
        _eggAvatar.enabled = true;
        this.OwnerEggModel.avatar = this._eggAvatar.sprite;
    }


    private void OnOpenEgg()
    {
        if (OwnerEggModel != null)
        {
            PopupManager.Instance.OnShowPopup(PopupType.OpenEgg, new NewEggPopupData()
            {
                model = OwnerEggModel
            }, false);
        }
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
