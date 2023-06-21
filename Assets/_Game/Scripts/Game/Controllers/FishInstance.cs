using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Models;
using GameCore;

public class FishInstance : Touchable
{
    private const float MovementSpeed = 15;

    [SerializeField]
    private SkinnedMeshRenderer _skin;
    [SerializeField]
    private Shader _fishShader;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private FishAura aura;
    private Vector3 _targetPosition;
    private Vector3 _velocity;
    private bool _isMoving;
    [SerializeField] public Transform mouthPosition;
    public FishModel Model
    {
        get; protected set;
    }

    private void Start()
    {
        OnGetModelData();
        this.aura = this.GetComponentInChildren<FishAura>();
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
        var fishVisual = FishHelper.GetVisualType(model);
        if(fishVisual == EFishTypeVisual.Free)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        gameObject.SetActive(active);
    }

    public void SetPos(Vector3 pos)
    {
        if (!_isMoving)
            transform.position = pos;
    }
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetTargetPos(Vector3 pos)
    {
        _targetPosition = pos;
        _isMoving = true;
    }

    protected override void Update()
    {
        base.Update();
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, MovementSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - _targetPosition.x) < 0.5f)
            {
                _isMoving = false;
            }
        }
    }

    public void SetActiveObject(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void PlayRandomAnim()
    {
        if(FishHelper.GetVisualType(this.Model) == EFishTypeVisual.Free)
        {
            _animator.SetTrigger("Jump");
        }
        else
        {
            int randomNumber = UnityEngine.Random.Range((int)EFishAnim.Jump, (int)EFishAnim.SwimDown);
            _animator.SetInteger("animation",randomNumber);
            _animator.SetTrigger("Play");
        }

    }
    public void Feed()
    {
        Action callback =()=>
        {
            if(FishHelper.GetVisualType(this.Model) == EFishTypeVisual.Free)
            {
                var target = this.gameObject.transform.Find("TargetFeed").transform.position;
                LeanTween.moveLocal(this.gameObject, target, 0.4f)
                .setLoopPingPong(1)
                .setEaseInExpo();
                SoundManager.Instance.PlaySound(ESoundID.Feed);
            }
            else
            {
                _animator.SetInteger("animation",(int)EFishAnim.Yes);
                _animator.SetTrigger("Play");
                SoundManager.Instance.PlaySound(ESoundID.Feed);
            }

        };

        LeanTween.delayedCall(0.7f, callback);

    }


    public void SetupMaterials()
    {
        if(FishHelper.GetVisualType(this.Model) != EFishTypeVisual.Free)
        {
            this._skin.material = this.CreateAndGetMaterial();
        }
        
    }

    public Material CreateAndGetMaterial()
    {
        Material mat = new Material(this._fishShader);
        Texture tex = FishAssetInit.Instance.GetFishTexture(FishHelper.GetVisualType(this.Model), this.Model.rarity);
        mat.SetTexture("_MainTex", tex);
        // mat.SetTexture("_FalloffSampler", FishAssetInit.Instance.textureFalloffControl);
        mat.SetTexture("_RimLightSampler", FishAssetInit.Instance.textureRimLightControl);
        Color shadowColor = new Color(1, 1, 1, 1);
        mat.SetColor("_ShadowColor", shadowColor);
        mat.SetFloat("Outline Thickness", 8);

        return mat;
    }

    private void OnGetModelData()
    {
        if (this._skin == null)
        {
            this._skin = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        if (this._animator == null)
        {
            this._animator = this.gameObject.GetComponent<Animator>();
        }
        if (this._fishShader == null)
        {
            this._fishShader = Shader.Find("UnityChan/Skin");
        }
    }

    protected override void OnTouch()
    {
        this.PlayRandomAnim();
        SoundManager.Instance.PlaySound(ESoundID.FishInteractive);
    }

    public void SetupAura()
    {

        if(this.aura != null)
        {
            this.aura.SetColor(this.Model);
        }
        
    }
}
