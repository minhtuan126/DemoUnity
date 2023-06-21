using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using GameCore.Models;

public class GameInitFollow : MonoBehaviour
{
    void Start()
    {
        LoadUserData().Forget();
    }


    private async UniTaskVoid LoadUserData()
    {
        GameManager.ResetData();

        await UniTask.Delay(1000);

        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Home);
    }
}
