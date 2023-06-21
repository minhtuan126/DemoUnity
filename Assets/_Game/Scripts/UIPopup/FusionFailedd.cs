using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Popup
{
public class FusionFailedd : PopupBase
{
    [SerializeField] private Image _fish1Avatar;

    [SerializeField] private Image _fish2Avatar;

    [SerializeField] private Button _btnOK;

    public FishModel fish1 { get; private set; }

    public FishModel fish2 { get; private set; }

    public override void OnShow(PopupInputData popupData)
    {
        base.OnShow(popupData);
        fish1 = GetPopupInputData<FusionFailData>().fish1;
        fish2 = GetPopupInputData<FusionFailData>().fish2;
    }

    protected override void Start()
    {
        base.Start();
        _btnOK.onClick.AddListener(()=>this.OnHide());
    }

    protected override void FieldData()
    {
        _fish1Avatar.sprite = fish1.avatar;
        _fish2Avatar.sprite = fish2.avatar;
    }
}
    public class FusionFailData : PopupInputData
    {
        public FishModel fish1;
        public FishModel fish2;
    }
}