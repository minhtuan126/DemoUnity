using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float speed = 10;
    private float time = 0.5f;
    private Vector3 targetPos = new Vector3(0,2,10);
    private Vector3 startPos = new Vector3(-5,8,0);
    private float startTime;
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, targetPos);
    }

    public void MoveFood(Vector3 target)
    {
        LeanTween.moveX(gameObject,target.x,1).setOnComplete(()=>
        {
            // EventManager.Invoke(FishEvent.PlayAnimEggPiece);
            RewardAnim.Instance.PlayEggPieceAnim();
            Destroy(this.gameObject);
        });
        LeanTween.moveY(gameObject,target.y,1).setEaseOutCirc();
        LeanTween.moveZ(gameObject, target.z,0.1f);
    }
}
