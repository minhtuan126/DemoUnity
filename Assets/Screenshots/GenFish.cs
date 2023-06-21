using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

[ExecuteInEditMode]
public class GenFish : MonoBehaviour
{

    public FishAssetInit fishAssetInit;

    public bool genFish = false;

    private void Update()
    {
        if (genFish)
        {

            genFish = false;

            for (int i = 1; i <= (int)Rarity.Epic; i++)
            {
                SpawnNewFish(new FishModel()
                {
                    gen = Gen.F0,
                    rarity = (Rarity)i,
                });

                SpawnNewFish(new FishModel()
                {
                    gen = Gen.F5,
                    rarity = (Rarity)i,
                });

                SpawnNewFish(new FishModel()
                {
                    gen = Gen.F9,
                    rarity = (Rarity)i,
                });

                SpawnNewFish(new FishModel()
                {
                    gen = Gen.F12,
                    rarity = (Rarity)i,
                });

                SpawnNewFish(new FishModel()
                {
                    gen = Gen.F15,
                    rarity = (Rarity)i,
                });
            }
        }
    }

    public void SpawnNewFish(FishModel model)
    {
        FishInstance prefab = fishAssetInit.SpawnNewFish(model, Vector3.zero, null, true);
        var eFishTypeVisual = FishHelper.GetVisualType(model);
        prefab.gameObject.name = $"{eFishTypeVisual}_{model.rarity}";
    }

}
