using System;
using System.Collections.Generic;
using UnityEngine;

public class EggTierData : ScriptableObject
{
    [Serializable]
    public struct EggData
    {
        [HideInInspector]
        public string name;
        [ReadOnly]
        public Rarity tier;
        public Sprite avatar;
    }

    public EggData[] eggData;

    private async void OnValidate()
    {
        var count = (int)Rarity.Epic + 1;
        Array.Resize(ref eggData, count);

        for (int i = 0; i < eggData.Length; i++)
        {
            eggData[i].tier = (Rarity)i;
            eggData[i].name = eggData[i].tier.ToString();
        }
    }

}
