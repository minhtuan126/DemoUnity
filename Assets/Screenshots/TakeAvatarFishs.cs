using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class TakeAvatarFishs : MonoBehaviour
{
    public TriggerCapture triggerCapture;

    public bool takeScreen = false;

    public FishInstance[] allFishs;

    // private void OnValidate()
    // {
    //     allFishs = GetComponentsInChildren<FishInstance>();
    // }
    // Update is called once per frame
    void Update()
    {
        if (takeScreen)
        {
            takeScreen = false;
            Take();
        }
    }

    void Take()
    {
        Taking().Forget();
    }


    public async UniTask Taking()
    {
        foreach (var fish in allFishs)
        {
            fish.SetActive(false);
        }

        foreach (var fish in allFishs)
        {
            fish.SetActive(true);
            triggerCapture.OnTakeScreen(fish.gameObject.name);

            await UniTask.Delay(1000);
            fish.SetActive(false);
            await UniTask.Delay(1000);
        }

    }
}
