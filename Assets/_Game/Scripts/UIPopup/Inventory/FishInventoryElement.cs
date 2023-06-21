using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using System.Linq;

public class FishInventoryElement : MonoBehaviour
{
    [SerializeField]
    private Image _fishAvatarBG;

    [SerializeField]
    private List<RarityBG> _rarityBGs;


    [SerializeField]
    private Image _fishAvatar;

    [Header("Tier")]
    [SerializeField]
    private TextMeshProUGUI _tierText;

    [SerializeField]
    private Image _tierImage;


    [Header("Gen")]
    [SerializeField]
    private TextMeshProUGUI _genText;

    [SerializeField]
    private Image _genImage;

    [Header("Selection")]

    [SerializeField]
    private Toggle _toggle;

    private bool _isSelected;
    private FishPageInventory _ownerPage;

    private FishInstance _fishInstance;

    public FishModel OwnerFishModel { get; protected set; }

    public Sprite FishAvatar { get; protected set; }

    public void SetActive(bool canSelected)
    {
        _toggle.interactable = canSelected;
    }

    // Start is called before the first frame update
    public void Init(FishPageInventory ownerPage, FishModel ownerFishModel, ToggleGroup toggleGroup)
    {
        _ownerPage = ownerPage;
        _toggle.group = toggleGroup;
        OwnerFishModel = ownerFishModel;
        

        var color = TierColor.TierColors[OwnerFishModel.rarity];
        _fishAvatarBG.sprite = DataReferece.Instance.GetSpriteRarity(ownerFishModel.rarity);
        _tierImage.color = color;
        _tierText.SetText(OwnerFishModel.rarity.ToString());
        _genText.SetText(OwnerFishModel.gen.ToString());
        _fishInstance = FishAssetInit.Instance.GetFishInstance(ownerFishModel);
        FishAvatar = ownerFishModel.avatar;
        _fishAvatar.sprite = FishAvatar;
    }

    // Update is called once per frame
    public void OnToggleSelected(bool isOn)
    {
        if (isOn)
        _ownerPage.OnSelectFish(this);
        _toggle.isOn = isOn;
    }
}
