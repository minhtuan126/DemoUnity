using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;
using UnityEngine.UI;

public class RewardAnim : MonoBehaviour
{
    public static RewardAnim Instance;
    [SerializeField] GameObject prefabEggPieces;
    [SerializeField] int maxPrefab = 10; 
    Queue<GameObject> eggPiecesQueue = new Queue<GameObject>();
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] int maxFlip = 1;
    [SerializeField] int minFlip = 7;
    [SerializeField] Transform target;
    [SerializeField] Transform spawnPosition;
    [SerializeField] LeanTweenType easeType;
    [SerializeField] float raidusRandomCirlce = 300;
    [SerializeField] List<Sprite> listSpriteEggPiece = new List<Sprite>();
    Vector3 targetPosition;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        EventManager.AddListener(FishEvent.PlayAnimEggPiece,()=>this.PlayEggPieceAnim());
        this.targetPosition = this.target.position;
        this.PrepareEggPieces();
    }

    private void PrepareEggPieces()
    {
        for (int i = 0; i < this.maxPrefab; i++)
        {
            GameObject eggPiece = this.target.GetComponent<GameObject>();
            eggPiece = Instantiate(this.prefabEggPieces);
            eggPiece.transform.parent = transform;
            eggPiece.SetActive(false);
            eggPiece.transform.localScale = Vector3.one;
            this.eggPiecesQueue.Enqueue(eggPiece);
        }
    }

    private void Animate(int amount)
    {
        Debug.Log(amount);
        Vector3 target = this.target.GetComponent<RectTransform>().position;
        for (int i = 0; i < amount; i++)
        {
            if(eggPiecesQueue.Count > 0)
            {
                float duration0 = UnityEngine.Random.Range(this.minAnimDuration, this.maxAnimDuration);
                float duration = UnityEngine.Random.Range(this.minAnimDuration, this.maxAnimDuration);
                int flipCount = UnityEngine.Random.Range(this.minFlip, this.maxFlip);
                int randomSprite = UnityEngine.Random.Range(0,this.listSpriteEggPiece.Count);
                GameObject eggPiece = this.eggPiecesQueue.Dequeue();
                eggPiece.SetActive(true);
                eggPiece.GetComponent<RectTransform>().anchoredPosition = this.GetRandomPostionWithinCircle(this.spawnPosition.position);
                if(this.listSpriteEggPiece.Count != 0)
                {
                    eggPiece.GetComponent<Image>().sprite = this.listSpriteEggPiece[randomSprite];
                }
                eggPiece.transform.localEulerAngles = Vector3.zero;

                Action process2 = ()=>
                {
                    LeanTween.move(eggPiece, target, duration)
                    .setEase(this.easeType)
                    .setOnComplete(()=>
                    {
                        eggPiece.SetActive(false);
                        this.eggPiecesQueue.Enqueue(eggPiece);
                        EventManager.Invoke(EResourceEvent.UpdateEggPieces);
                    });
                }; 

                LeanTween.rotateY(eggPiece, 360 * flipCount, duration0).setOnComplete(process2);
            }
        }
    }
    // public void AddEggPieces(int amount)
    // {
    //     this.Animate(amount);
    // }

    public void PlayEggPieceAnim(int amount = 5)
    {
        //Debug.Log("Debug.Log(amount);:" + amount);
        this.Animate(amount);
    }

    public Vector3 GetRandomPostionWithinCircle(Vector3 center)
    {
        var newRandomCircle = UnityEngine.Random.insideUnitCircle * this.raidusRandomCirlce;
        var result = new Vector3(center.x + newRandomCircle.x, center.y + newRandomCircle.y, 0);
        return result;
    }
}
