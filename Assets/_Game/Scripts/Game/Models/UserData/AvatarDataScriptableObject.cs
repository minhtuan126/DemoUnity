using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AvatarDataScriptableObject", menuName = "ScriptableObjects/AvatarDataScriptableObject", order = 1)]
public class AvatarDataScriptableObject : ScriptableObject
{
    public AvatarData[] avatarDatas;
}

[System.Serializable]
public struct AvatarData
{
    public int id;
    public Sprite avatarSprite;

}
