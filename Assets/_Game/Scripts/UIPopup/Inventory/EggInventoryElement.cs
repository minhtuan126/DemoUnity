using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
public class EggInventoryElement : MonoBehaviour
{
    [SerializeField]
    public Image _eggAvatar;
    [SerializeField]
    private Image _eggAvatarBG;

    [Header("Selection")]

    [SerializeField]
    private Toggle _toggle;

    private bool _isSelected;
    private EggPageInventoty _ownerPage;

    public FishModel OwnerEggModel { get; protected set; }

    public List<Sprite> eggSprites = new List<Sprite>();


    // Start is called before the first frame update
    public void Init(EggPageInventoty ownerPage, FishModel ownerEggModel, ToggleGroup toggleGroup)
    {
        _ownerPage = ownerPage;
        _toggle.group = toggleGroup;
        OwnerEggModel = ownerEggModel;
        _toggle.isOn = false;

        var color = TierColor.TierColors[OwnerEggModel.rarity];
        // _eggAvatarBG.color = color;
        //_eggAvatar.sprite = ownerEggModel.avatar;
        switch(OwnerEggModel.category)
        {
            case 0:_eggAvatar.sprite = eggSprites[0];break;
            case 1:_eggAvatar.sprite = eggSprites[1];break;
            case 2:_eggAvatar.sprite = eggSprites[2];break;
            case 3:_eggAvatar.sprite = eggSprites[3];break;
        }
    }

    // Update is called once per frame
    public void OnToggleSelected(bool isOn)
    {
        if (isOn)
        {
            _ownerPage.OnSelectEgg(this);
        }
        
        _toggle.isOn = isOn;
    }
}
