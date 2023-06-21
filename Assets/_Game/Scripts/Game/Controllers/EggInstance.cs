using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Models;
using GameCore;

public class EggInstance : MonoBehaviour
{
    [SerializeField]

    private Sprite _avatarSprite;
    [SerializeField]
    private Animator _animator;
    public FishModel Model
    {
        get; protected set;
    }

    public Sprite Avatar
    {
        get { return _avatarSprite; }
    }

    private void Start()
    {
        OnGetModelData();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        OnGetModelData();
    }

#endif
    // Start is called before the first frame update
    public void Init(FishModel model, bool active = true)
    {
        Debug.Assert(model != null);
        Model = model;
        gameObject.name = model.name;
        transform.rotation = Quaternion.Euler(0, -120, 0);
        gameObject.SetActive(active);
        model.avatar = _avatarSprite;
        _animator = GetComponent<Animator>();
    }

    public void Feed()
    {
        _animator.SetTrigger("Feed");
    }


    private void OnGetModelData()
    {
        if (this._animator == null)
        {
            this._animator = this.gameObject.GetComponent<Animator>();
        }
    }
}
