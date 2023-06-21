using GameCore.Models;
using UnityEngine;

[CreateAssetMenu(fileName = "FishConfig", menuName = "ScriptableObjects/FishConfig", order = 1)]
public class FishConfig : ScriptableObject
{
    // Start is called before the first frame update
    public FishDataConfig fishData;
}
