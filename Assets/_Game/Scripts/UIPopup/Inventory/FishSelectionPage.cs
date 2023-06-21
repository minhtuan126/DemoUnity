using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using Cysharp.Threading.Tasks;

public class FishSelectionPage : MonoBehaviour
{
    [SerializeField]
    private RectTransform fishPageContent;
    [SerializeField]
    private FishSelectionElement _fishInventoryElementPrefab;

    [SerializeField]
    private ToggleGroup _toggleGroup;
    [SerializeField]
    private TextMeshProUGUI _textEmpty;
    private List<FishSelectionElement> _fishInventoryElements = new List<FishSelectionElement>();
    public FishSelectionElement FishInventoryElementSelected { get; private set; }
    private bool _isInit = false;
    // Update is called once per frame
    public void OnSelectFish(FishSelectionElement selected)
    {
        FishInventoryElementSelected = selected;
    }

    private void OnInit()
    {
        // if (!_isInit)
        {
            Clean();

            _isInit = true;
            var fishs = GameManager.UserData.fishList;
            foreach (var fish in fishs)
            {
                var newElement = Instantiate(_fishInventoryElementPrefab, fishPageContent);
                newElement.Init(this, fish, _toggleGroup);
                _fishInventoryElements.Add(newElement);
            }
        }
    }

    public void OnShow(FishModel fishFilter, FishModel fishTarget)
    {
        OnInit();
        FishInventoryElementSelected = null;
        bool hasFish = false;
        foreach (var fishElement in _fishInventoryElements)
        {
            var fish = fishElement.OwnerFishModel;
            if ((fishFilter == null) || (fish != fishFilter && fish.rarity == fishFilter.rarity && fishFilter.gen == fish.gen && fish.tokenId != -1))
            {
                if (fish.tokenId != -1)
                {
                    fishElement.SetActive(true);
                    hasFish = true;
                }                    
                else
                    fishElement.SetActive(false);
            }
            else
            {
                fishElement.SetActive(false);
            }
            if(fish.gen == Gen.F15)
            {
                fishElement.SetActive(false);
            }
        }
        if (hasFish)
        {
            _textEmpty.SetActive(false);
        } else
        {
            _textEmpty.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    private void Clean()
    {
        for (int i = 0; i < _fishInventoryElements.Count; i++)
        {
            Destroy(_fishInventoryElements[i].gameObject);
        }
        _fishInventoryElements.Clear();
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
