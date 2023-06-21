using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;
using Cysharp.Threading.Tasks;

public class FishPageInventory : MonoBehaviour
{
    [SerializeField]
    private RectTransform fishPageContent;
    [SerializeField]
    private FishInventoryElement _fishInventoryElementPrefab;

    [SerializeField]
    private FishInventorySelected _fishInventorySelected;
    [SerializeField]
    private ToggleGroup _toggleGroup;
    [SerializeField]
    private GameObject _noFish;
    private List<FishInventoryElement> _fishInventoryElements = new List<FishInventoryElement>();
    public FishInventoryElement FishInventoryElementSelected { get; private set; }

    private bool _isInit = false;
    // Update is called once per frame
    public void OnSelectFish(FishInventoryElement selected)
    {
        FishInventoryElementSelected = selected;
        _fishInventorySelected.OnSelectedFish(FishInventoryElementSelected);
    }


    public void OnShowWithFilder(FishModel fishFilter, FishModel fishTarget)
    {
        if (!_isInit)
        {
            _isInit = true;
            var fishs = GameManager.UserData.fishList;
            foreach (var fish in fishs)
            {
                var newElement = Instantiate(_fishInventoryElementPrefab, fishPageContent);
                newElement.Init(this, fish, _toggleGroup);
                _fishInventoryElements.Add(newElement);
            }
        }

        foreach (var fishElement in _fishInventoryElements)
        {
            var fish = fishElement.OwnerFishModel;
            if ((fishFilter == null) || (fish.tokenId != -1 && fish != fishFilter && fish.rarity == fishFilter.rarity && fishFilter.gen == fish.gen))
            {
                fishElement.SetActive(true);
                if (fishTarget == fish)
                {
                    fishElement.OnToggleSelected(true);
                }
            }
            else
            {
                fishElement.SetActive(false);
            }
        }


        gameObject.SetActive(true);
    }

    public async UniTask OnShow(FishModel fishSelected = null, bool clean = true, bool isShow = true)
    {
        if (clean)
        {
            Clean();
        }
        await UniTask.Yield();

        var fishs = GameManager.UserData.fishList;
        foreach (var fish in fishs)
        {
            var newElement = Instantiate(_fishInventoryElementPrefab, fishPageContent);
            newElement.Init(this, fish, _toggleGroup);
            _fishInventoryElements.Add(newElement);
        }

        await UniTask.Yield();
        gameObject.SetActive(isShow);
        foreach (var fishElement in _fishInventoryElements)
        {
            if (fishElement.OwnerFishModel == fishSelected)
            {
                fishElement.OnToggleSelected(true);
            }
        }

        if (_fishInventoryElements.Count > 0)
        {
            this._noFish.SetActive(false);
            this._fishInventorySelected.SetActive(true);
            this.fishPageContent.SetActive(true);

        }
        else
        {
            this._noFish.SetActive(true);
            this._fishInventorySelected.SetActive(false);
            this.fishPageContent.SetActive(false);
        }
    }

    private void Clean()
    {
        // TODO:disable and re-use
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


    public void OnShowFilter(int rarity, int gen)
    {
        // TODO:disable and re-use
        for (int i = 0; i < _fishInventoryElements.Count; i++)
        {
            Destroy(_fishInventoryElements[i].gameObject);
        }

        _fishInventoryElements.Clear();

        var fishs = GameManager.UserData.fishList.FindAll((element)=> (int)element.rarity == rarity && (int)element.gen == gen);
        foreach (var fish in fishs)
        {
            var newElement = Instantiate(_fishInventoryElementPrefab, fishPageContent);
            newElement.Init(this, fish, _toggleGroup);
            _fishInventoryElements.Add(newElement);
        }
    }
}
