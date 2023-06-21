using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCore.Models;
using TMPro;

public class FishSelection : MonoBehaviour
{

    private readonly Vector3 rightFishPos = new Vector3(30, 0, 10);
    private readonly Vector3 leftFishPos = new Vector3(-30, 0, 10);
    private readonly Vector3 centerPos = new Vector3(0, 0, 10);

    [SerializeField]
    private Button _buttonLeft;
    [SerializeField]
    private Button _buttonRight;
    [SerializeField]
    private Image _rarityImg;
    [SerializeField]
    private TextMeshProUGUI _genText;
    [SerializeField]
    private TextMeshProUGUI _rarityText;
    public List<FishInstance> FishInstances { get; private set; } = new List<FishInstance>();
    public FishInstance CurrentFish { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        _buttonLeft.onClick.AddListener(OnBack);
        _buttonRight.onClick.AddListener(OnNext);

        EventManager.AddListener(FishEvent.SpawnAllFish, OnSpawnAllFishs);
        EventManager.AddListener<FishModel>(FishEvent.SelectMainFish, OnSelectFish);
        EventManager.AddListener<FishModel, bool>(FishEvent.SpawnNewFishInstance, this.SpawnNewFishInstance);
        EventManager.AddListener<int>(FishEvent.RemoveFishInstance, this.RemoveFishInstance);
    }

    public void OnSpawnAllFishs()
    {
        var fishList = GameManager.UserData.fishList;
        if (fishList.Count < 2)
        {
            _buttonLeft.gameObject.SetActive(false);
            _buttonRight.gameObject.SetActive(false);
        }
        if (fishList == null) return;

        foreach (var fish in fishList)
        {
            var newFish = FishAssetInit.Instance.SpawnNewFish(fish, rightFishPos, null, true);
            FishInstances.Add(newFish);
        }

        CurrentFish = FishInstances[0];
        _rarityImg.sprite = DataReferece.Instance.GetBGTextRarity(CurrentFish.Model.rarity);
        _genText.text = CurrentFish.Model.gen.ToString();
        _rarityText.text = CurrentFish.Model.rarity.ToString();
        MainGameUI.OnChangeFish.Invoke(CurrentFish);
        CurrentFish.transform.position = centerPos;
    }

    public void OnNext()
    {
        OnSelectFish(GameManager.UserData.OnNextFish(), leftFishPos, rightFishPos);
    }

    public void OnBack()
    {
        OnSelectFish(GameManager.UserData.OnBackFish(), rightFishPos, leftFishPos);
    }

    public void OnSelectFish(FishModel fish, Vector3 moveTo, Vector3 moveFrom)
    {
        foreach (var fishInstance in FishInstances)
        {
            if (fishInstance.Model == fish)
            {
                if (CurrentFish != null)
                {
                    CurrentFish.SetTargetPos(moveTo);
                }
                CurrentFish = fishInstance;
                MainGameUI.OnChangeFish.Invoke(fishInstance);
                CurrentFish.SetPos(moveFrom);
                CurrentFish.SetTargetPos(centerPos);
                _rarityImg.sprite = DataReferece.Instance.GetBGTextRarity(CurrentFish.Model.rarity);
                _genText.text = CurrentFish.Model.gen.ToString();
                _rarityText.text = CurrentFish.Model.rarity.ToString();
            }
        }
    }

    public void OnSelectFish(FishModel fish)
    {
        while (fish != GameManager.UserData.OnNextFish())
        {
        }

        OnSelectFish(fish, leftFishPos, rightFishPos);
    }

    public void RemoveFishInstance(int tokenID)
    {
        FishInstance fish = this.FishInstances.Find((fishInstance) => fishInstance.Model.tokenId == tokenID);
        if (fish != null)
        {
            Destroy(fish.gameObject);
        }

    }

    public void SpawnNewFishInstance(FishModel fishModel, bool spawnAtCenter = false)
    {
        var newFish = FishAssetInit.Instance.SpawnNewFish(fishModel, spawnAtCenter ? Vector3.zero : rightFishPos, null, true);
        _buttonLeft.gameObject.SetActive(true);
        _buttonRight.gameObject.SetActive(true);

        if (spawnAtCenter)
        {
            // WORK WRONG NEED TO FIX
            OnSelectFish(fishModel, leftFishPos, rightFishPos);
            //
        }
        FishInstances.Add(newFish);
    }

}
