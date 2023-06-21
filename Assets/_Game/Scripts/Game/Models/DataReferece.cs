using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameCore.Models;
using System;

public class DataReferece : SingletonPersistent<DataReferece>
{
    [SerializeField]
    private AvatarDataScriptableObject _avatarData;
    [SerializeField]
    private RarityBG[] arrTierTextBG;
    public Dictionary<int, Sprite> AvatarDataDict { get; private set; } = new Dictionary<int, Sprite>();

    private void OnValidate()
    {
        var count = (int)Rarity.None + 1;
        Array.Resize(ref arrTierTextBG, count);
        for (int i = 0; i < arrTierTextBG.Length; i++)
        {
            arrTierTextBG[i].rarity = (Rarity)i;
        }
    }

    private void Start()
    {
        Debug.Assert(_avatarData != null);
        InitAvatarData(_avatarData.avatarDatas);
    }

    public void InitAvatarData(AvatarData[] avatarData)
    {
        AvatarDataDict.Clear();

        foreach (var item in avatarData)
        {
            if (!AvatarDataDict.ContainsKey(item.id))
            {
                AvatarDataDict.Add(item.id, item.avatarSprite);
            }
        }
    }

    public Sprite GetUserAvatarSprite(int id)
    {
        Sprite avatar = null;
        AvatarDataDict.TryGetValue(id, out avatar);
        return avatar;
    }

    public void UpdatedUserData()
    {
        GameManager.UserData.avatar = GetUserAvatarSprite(GameManager.UserData.avatar_id);
    }

    public Sprite GetSpriteRarity(Rarity rarity)
    {
        return arrTierTextBG[(int)rarity].avatarBG;
    }

    public Sprite GetBGTextRarity(Rarity rarity)
    {
        return arrTierTextBG[(int)rarity].tierTextBG;
    }
    public RarityBG GetRarityData(Rarity rarity)
    {
        return arrTierTextBG[(int)rarity];
    }
}

[Serializable]
public struct RarityBG
{
    public Rarity rarity;
    public Sprite avatarBG;
    public Sprite tierTextBG;

}
