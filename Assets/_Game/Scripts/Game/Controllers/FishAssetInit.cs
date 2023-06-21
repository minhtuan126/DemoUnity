using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameCore.Models;
using UnityEngine;

public class FishAssetInit : SingletonPersistent<FishAssetInit>
{
    private readonly Vector3 StartFishPosition = new Vector3(-500, 0, 0);
    public FishAvatarsConfig fishAvatarsConfig;
    public AllEggDataConfig eggDataConfigs;
    private Dictionary<EFishTypeVisual, Sprite[]> _fishAvatarDict = new Dictionary<EFishTypeVisual, Sprite[]>();
    private Dictionary<Rarity, EggInstance> _eggDict = new Dictionary<Rarity, EggInstance>();
    private Dictionary<FishModel, FishInstance> _allFishInstance = new Dictionary<FishModel, FishInstance>();
    public List<Texture> listTextureDog;
    public List<Texture> listTextureRabbit;
    public List<Texture> listTextureTiger;
    public List<Texture> listTextureCat;
    public List<Texture> listTexturePanda;
    public Texture textureFalloffControl;
    public Texture textureRimLightControl;
    [SerializeField]
    private FishPrefabData[] _fishPrefabData;

    public Sprite[] eggSprites;

    private bool _hasInstanced = false;

#if UNITY_EDITOR
    private void OnValidate()
    {
        Array.Resize(ref _fishPrefabData, (int)EFishTypeVisual.Count);
        for (int i = 0; i < _fishPrefabData.Length; i++)
        {
            _fishPrefabData[i].eFshTypeVisual = (EFishTypeVisual)i;
        }

        Array.Resize(ref eggSprites, (int)Rarity.None);
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        _fishAvatarDict = fishAvatarsConfig.GetData();

        for (int i = 0; i < eggDataConfigs.eggConfigs.Length; i++)
        {
            _eggDict.Add(eggDataConfigs.eggConfigs[i].rarity, eggDataConfigs.eggConfigs[i].eggPrefab);
        }
    }

    public FishInstance SpawnNewFish(FishModel model, Vector3 pos, Transform parent, bool active = true)
    {
        if (model == null) return null;
        EFishTypeVisual visualType = FishHelper.GetVisualType(model);
        FishInstance prefab = this.GetFishPrefab(visualType);

        InitFish(ref model);
        var entityObject = GameObject.Instantiate(prefab, pos, Quaternion.identity, parent);
        entityObject.gameObject.name = name;
        entityObject.Init(model, active);
        entityObject.SetupMaterials();
        entityObject.SetupAura();
        return entityObject;
    }

    public void OnSpawnAllFish()
    {
        if (_hasInstanced) return;

        var fishList = GameManager.UserData.fishList;
        if (fishList != null && fishList.Count > 0)
        {
            foreach (var fishModel in fishList)
            {
                OnSpawnFish(fishModel);
            }
        }

        var eggList = GameManager.UserData.eggList;
        if (eggList != null && eggList.Count > 0)
        {
            for (int i = 0; i < eggList.Count; i++)
            {
                var eggConfig = GetEggConfig(eggList[i]);
                if (eggConfig != null)
                    eggList[i].avatar = eggConfig.Avatar;
                else
                    Debug.LogError("Egg: " + eggList[i].rarity + " not found!");
            }
        }
    }

    public void InitFish(ref FishModel model)
    {
        if (model == null) return;

        EFishTypeVisual visualType = FishHelper.GetVisualType(model);
        model.avatar = GetFishAvatar(model);
    }

    public void InitEgg(ref FishModel model)
    {
        if (model == null) return;
        model.avatar = GetEggSprite(model.rarity);

    }
    public FishInstance OnSpawnFish(FishModel fishModel)
    {
        if (!_allFishInstance.ContainsKey(fishModel))
        {
            var fishInstance = SpawnNewFish(fishModel, StartFishPosition, transform);
            _allFishInstance.Add(fishModel, fishInstance);
            return fishInstance;
        }
        return _allFishInstance[fishModel];
    }

    public FishInstance GetFishInstance(FishModel model)
    {
        return OnSpawnFish(model);
    }

    public EggInstance GetEggConfig(FishModel model)
    {
        EggInstance config = null;
        _eggDict.TryGetValue(model.rarity, out config);
        return config;
    }

    public Texture GetFishTexture(EFishTypeVisual visualType, Rarity rarity)
    {
        Texture texture = null;
        switch (visualType)
        {
            case EFishTypeVisual.Cat:
                texture = this.listTextureCat[(int)rarity];
                break;

            case EFishTypeVisual.Dog:
                texture = this.listTextureDog[(int)rarity];
                break;

            case EFishTypeVisual.Panda:
                texture = this.listTexturePanda[(int)rarity];
                break;

            case EFishTypeVisual.Tiger:
                texture = this.listTextureTiger[(int)rarity];
                break;

            case EFishTypeVisual.Rabbit:
                texture = this.listTextureRabbit[(int)rarity];
                break;

            default:
                Debug.LogError(visualType);
                break;
        }

        return texture;
    }

    public Sprite GetEggSprite(Rarity rarity)
    {
        return eggSprites[(int)rarity];
    }


    private FishInstance GetFishPrefab(EFishTypeVisual visualType)
    {
        return _fishPrefabData[(int)visualType].fishPefab;
    }

    public Sprite GetFishAvatar(FishModel model)
    {
        EFishTypeVisual visualType = FishHelper.GetVisualType(model);
        return GetFishAvatar(visualType, model);
    }

    public Sprite GetFishAvatar(EFishTypeVisual visualType, FishModel model)
    {
        Sprite[] sprites;
        if (_fishAvatarDict.TryGetValue(visualType, out sprites))
        {
            var rarity = (int)model.rarity;
            if (rarity < sprites.Length)
                return sprites[rarity];
        }
        return null;
    }

    [Serializable]
    public struct FishPrefabData
    {
        [ReadOnly]
        public EFishTypeVisual eFshTypeVisual;
        public FishInstance fishPefab;
    }
}
