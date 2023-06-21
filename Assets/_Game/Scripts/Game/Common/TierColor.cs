using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TierColor
{
    public static readonly Dictionary<Rarity, Color> TierColors = new Dictionary<Rarity, Color>
    {
        { Rarity.Common, new Color(0.7f, 0.7f, 0.7f, 1)},
        { Rarity.Great,new Color(0, 1, 0, 1)},
        { Rarity.Rare,new Color(1, 0.4f, 0, 1)},
        { Rarity.Epic, new Color(1f, 0f, 0.7f, 1f)},
    };

    public static Color GetColor(Rarity tier)
    {
        return TierColor.TierColors[tier];
    }
}
