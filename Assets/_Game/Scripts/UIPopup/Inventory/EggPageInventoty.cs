using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Models;

public class EggPageInventoty : MonoBehaviour
{
    [SerializeField]
    private RectTransform _eggPageContent;
    [SerializeField]
    private EggInventoryElement _eggInventoryElementPrefab;

    [SerializeField]
    private EggInventorySelected _eggInventorySelected;
    [SerializeField]
    private ToggleGroup _toggleGroup;
    [SerializeField]
    private GameObject _noEgg;

    private List<EggInventoryElement> _eggInventoryElements = new List<EggInventoryElement>();

    public EggInventoryElement EggInventoryElementSelected { get; private set; }

    // Update is called once per frame
    public void OnSelectEgg(EggInventoryElement selected)
    {
        EggInventoryElementSelected = selected;
        _eggInventorySelected.OnSelectedEgg(EggInventoryElementSelected);
    }

    public void OnShow(FishModel egg)
    {
        OnShow();
        if (_eggInventoryElements != null && _eggInventoryElements.Count > 0)
        {
            if (egg != null)
            {
                foreach (var element in _eggInventoryElements)
                {
                    if (egg == element.OwnerEggModel)
                    {
                        element.OnToggleSelected(true);
                        break;
                    }
                }
            }
            else
            {
                _eggInventoryElements[0].OnToggleSelected(true);
            }
        }
    }

    public void OnShow()
    {
        // TODO:disable and re-use
        // for (int i = _eggInventoryElements.Count - 1; i  >= 0; i--)
        for (int i = 0; i < _eggInventoryElements.Count; i++)
        {
            Destroy(_eggInventoryElements[i].gameObject);
        }

        _eggInventoryElements.Clear();
        var eggs = GameManager.UserData.eggList;
        foreach (var egg in eggs)
        {
            var newElement = Instantiate(_eggInventoryElementPrefab, _eggPageContent);
            newElement.Init(this, egg, _toggleGroup);
            _eggInventoryElements.Add(newElement);
        }
        if (_eggInventoryElements.Count > 0)
        {
            this._noEgg.SetActive(false);
            this._eggInventorySelected.SetActive(true);
            this._eggPageContent.SetActive(true);
            _eggInventoryElements[0].OnToggleSelected(true);
        }
        else
        {
            this._noEgg.SetActive(true);
            this._eggInventorySelected.SetActive(false);
            this._eggPageContent.SetActive(false);
        }
        //foreach (var eggElement in _eggInventoryElements)
        //{
        //    if (fishElement.OwnerFishModel == fishSelected)
        //    {
        //        Debug.Log(fishElement.OwnerFishModel.tokenId);
        //        Debug.Log(fishSelected.tokenId);
        //        fishElement.OnToggleSelected(true);
        //    }
        //}
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {

    }

}
