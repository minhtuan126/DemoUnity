using System;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

[CreateAssetMenu(fileName = "FishAvatarsConfig", menuName = "ScriptableObjects/FishAvatarsConfig", order = 1)]
public class FishAvatarsConfig : ScriptableObject
{
    public FishAvatarData[] fishAvatarData;

#if UNITY_EDITOR
    private void OnValidate()
    {
        Array.Resize(ref fishAvatarData, (int)EFishTypeVisual.Count);
        for (int i = 0; i < fishAvatarData.Length; i++)
        {
            fishAvatarData[i].eFishTypeVisual = (EFishTypeVisual)i;
        }
    }
#endif

    public Dictionary<EFishTypeVisual, Sprite[]> GetData()
    {
        Dictionary<EFishTypeVisual, Sprite[]> fishAvatarDict = new Dictionary<EFishTypeVisual, Sprite[]>();
        for (int i = 0; i < fishAvatarData.Length; i++)
        {
            if (!fishAvatarDict.ContainsKey(fishAvatarData[i].eFishTypeVisual))
                fishAvatarDict.Add(fishAvatarData[i].eFishTypeVisual, fishAvatarData[i].avatars);
        }
        return fishAvatarDict;
    }
}

[Serializable]
public struct FishAvatarData
{
    [ReadOnly]
    public EFishTypeVisual eFishTypeVisual;
    public Sprite[] avatars;
}
