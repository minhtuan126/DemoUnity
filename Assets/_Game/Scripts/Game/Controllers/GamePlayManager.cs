using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using GameCore.Models;
public enum GamePlayEvent
{
    StartGame,

    EndGame,
}

public class GamePlayManager : SingletonScene<GamePlayManager>
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);

        EventManager.Invoke(GamePlayEvent.StartGame);

        EventManager.Invoke(FishEvent.SpawnAllFish);
    }
}
