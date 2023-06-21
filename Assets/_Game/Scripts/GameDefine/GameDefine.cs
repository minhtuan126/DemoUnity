using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

public enum Rarity
{
    Common,
    Great,
    Rare,
    Epic,
    None,
}

public enum Gen
{
    Egg,
    F0,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
}

public enum EFishRarity
{
    Common,
    Great,
    Rare,
    Epic
}

public enum EFishTypeVisual
{
    Free,
    Cat,
    Rabbit,
    Dog,
    Panda,
    Tiger,

    Count,
}

public class FishHelper
{
    public static EFishTypeVisual GetVisualType(FishModel model)
    {
        var gen = (int)model.gen;

        if (model.tokenId == -1)
        {
            return EFishTypeVisual.Free;
        }
        if (gen >= 1 && gen <= 4)
        {
            return EFishTypeVisual.Cat;
        }
        else if (gen > 4 && gen <= 8)
        {
            return EFishTypeVisual.Rabbit;
        }
        else if (gen > 8 && gen <= 12)
        {
            return EFishTypeVisual.Dog;
        }
        else if (gen > 12 && gen <= 15)
        {
            return EFishTypeVisual.Panda;
        }
        else
        {
            return EFishTypeVisual.Tiger;
        }
    }
}

public enum EFishAnim
{
    Idle,
    Jump,
    Yes,
    No,
    Damage,
    Attack,
    Die,
    Swim,
    SwimR,
    SwimL,
    SwimUp,
    SwimDown

}

public class GameDefine
{
    public static readonly bool isTest = true;
}
