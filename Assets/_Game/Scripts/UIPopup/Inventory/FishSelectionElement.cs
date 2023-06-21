using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using System.Linq;

public class FishSelectionElement : MonoBehaviour
{
    [SerializeField]
    private Image _fishAvatarBG;

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

    [Header("Selection")]

    [SerializeField]
    private Toggle _toggle;

    private FishSelectionPage _ownerPage;

    private FishInstance _fishInstance;

    public FishModel OwnerFishModel { get; protected set; }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public void SetOn(bool isOn)
    {
        _toggle.isOn = isOn;
    }
    // Start is called before the first frame update
    public void Init(FishSelectionPage ownerPage, FishModel ownerFishModel, ToggleGroup toggleGroup)
    {
        _ownerPage = ownerPage;
        _toggle.group = toggleGroup;
        OwnerFishModel = ownerFishModel;
        _toggle.isOn = false;

        var color = TierColor.TierColors[OwnerFishModel.rarity];
        _fishAvatarBG.sprite = DataReferece.Instance.GetSpriteRarity(ownerFishModel.rarity);
        _tierImage.color = color;
        _tierText.SetText(OwnerFishModel.rarity.ToString());
        _genText.SetText(OwnerFishModel.gen.ToString());
        _fishInstance = FishAssetInit.Instance.GetFishInstance(ownerFishModel);
        _fishAvatar.sprite = ownerFishModel.avatar;
    }

    // Update is called once per frame
    public void OnToggleSelected(bool isOn)
    {
        _ownerPage.OnSelectFish(this);
        _toggle.isOn = isOn;
    }
}
