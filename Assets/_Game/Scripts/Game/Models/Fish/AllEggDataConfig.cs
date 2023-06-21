using System;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEggDataConfig", menuName = "ScriptableObjects/AllEggDataConfig", order = 1)]
public class AllEggDataConfig : ScriptableObject
{
    // Start is called before the first frame update
    public EggData[] eggConfigs;

    private void OnValidate()
    {
        int count = (int)Rarity.Epic + 1;
        Array.Resize(ref eggConfigs, count);
        for (int i = 0; i < count; i++)
        {
            eggConfigs[i].rarity = (Rarity)i;
        }
    }
}

[System.Serializable]
public struct EggData
{
    [ReadOnly]
    public Rarity rarity;
    public EggInstance eggPrefab;
}
