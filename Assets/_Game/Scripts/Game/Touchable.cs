using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Touchable : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] protected float x0 = 0;
    [SerializeField] protected float xPrevious = 0;

    [SerializeField] protected float x1 = 0;

    [SerializeField] protected bool isTouching;

    [SerializeField] protected bool isMobile;

    [SerializeField] protected float rotationSpeed = 400;

    [SerializeField] private float offset = 0;
    [SerializeField]  GameObject objectToRotate;

    private Vector3 rotateDir = new Vector3(0, -1, 0);



    protected virtual void Awake()
    {
        this.isMobile = false;
        #if UNITY_ANDROID
            this.isMobile = true;
        #endif
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(this.isMobile && !GameDefine.isTest) // Mobile
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                x0 = Input.GetTouch(0).position.x;
                if(Physics.Raycast(ray, out hit))
                {
                    this.isTouching = true;
                }
                this.isTouching = true;
            }
            if(this.isTouching)
            {
                this.TouchMobile();
            }
        } // end of mobile
        
        else // Desktop
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                x0 = Input.mousePosition.x;
                if(Physics.Raycast(ray, out hit))
                {
                    this.isTouching = true;
                }
            }
            if(this.isTouching)
            {
                this.TouchDesktop();
            }  
        } // end of desktop
        
    } // end of update

    protected virtual void OnDraging()
    {
        if(this.objectToRotate != null)
        {
            this.objectToRotate.transform.Rotate(this.rotateDir * this.rotationSpeed * Time.deltaTime * this.offset);
        }
        else
        {
            this.transform.Rotate(this.rotateDir * this.rotationSpeed * Time.deltaTime * this.offset);
        }
        
    }

    protected virtual void OnTouch()
    {
        
    }

    private void TouchDesktop()
    {
        this.xPrevious = this.x1;     
        this.x1 = Input.mousePosition.x;
        if(x0 != x1)
        {
            this.offset = x1 - xPrevious;
            this.OnDraging();
        }

        if(Input.GetMouseButtonUp(0))
        {
            this.isTouching = false;
            x1 = Input.mousePosition.x;
            if(x1 - x0 == 0)
            {
                this.OnTouch();
            }
        }
    }

    private void TouchMobile()
    {
        this.xPrevious = this.x1;     
        this.x1 = Input.GetTouch(0).position.x;
        if(x0 != x1)
        {
            this.offset = x1 - xPrevious;
            this.OnDraging();
        }

        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            this.isTouching = false;
            x1 = Input.GetTouch(0).position.x;
            if(x1 - x0 == 0)
            {
                this.OnTouch();
            }
        }
    }

}
