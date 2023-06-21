using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class MouseEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Queue<GameObject> queue = new Queue<GameObject>();
    [SerializeField] int maxClickEffect = 5;
    [SerializeField] float clickEffectDuration = 0.75f;
    public GameObject clickEffect;
    public GameObject dragEffect;
    public RectTransform dragRT;
    public RectTransform rt;
    public ParticleSystem ps;
    public Canvas canvas;
    public bool isDragging = false;
    private Vector3 newAnchorPosition = Vector3.zero;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitDragEffect();
        rt = GetComponent<RectTransform>();
        var UICanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        this.canvas = UICanvas;
        this.InitPoolClickEffect();
        
    }

    private void InitPoolClickEffect()
    {
        for (int i = 0; i < this.maxClickEffect; i++)
        {
            var clickObj = Instantiate<GameObject>(this.clickEffect);
            clickObj.transform.parent = this.transform;
            clickObj.SetActive(false);
            this.queue.Enqueue(clickObj);
        }
    }

    private GameObject GetClickObjectFromPool()
    {
        if(this.queue.Count == 0)
        {
            var clickObject = Instantiate<GameObject>(this.clickEffect);
            clickObject.transform.parent = this.transform;
            clickObject.SetActive(true);
            this.queue.Enqueue(clickObject);
        }
        GameObject clickObj = this.queue.Dequeue();
        clickObj.SetActive(true);
        LeanTween.delayedCall(this.clickEffectDuration,()=>this.Repool(clickObj));
        return clickObj;
    }

    private void Repool(GameObject obj)
    {
        if(obj != null)
        {
            obj.SetActive(false);
            this.queue.Enqueue(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            this.newAnchorPosition = ScreenToCanvasPosition(Input.mousePosition);
            // var newObj = GameObject.Instantiate(clickEffect);
            var clickObj = this.GetClickObjectFromPool();
            clickObj.transform.SetParent(rt);
            clickObj.GetComponent<RectTransform>().anchoredPosition = newAnchorPosition;
            SoundManager.Instance.PlaySound(ESoundID.ButtonClick);
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        OnDragging();
    }

    private void SetParentTransform()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("MouseEffect").transform);
    }

    public void InitDragEffect()
    {
        this.dragEffect = Instantiate(dragEffect, Vector3.zero, Quaternion.identity, transform);
        dragRT = dragEffect.GetComponent<RectTransform>();
        ps = dragEffect.GetComponent<ParticleSystem>();
        dragRT.anchoredPosition = Vector2.zero;
        
    }

    public void OnDragging()
    {
        
        if(isDragging)
        {
            Vector3 newDragPosition = ScreenToCanvasPosition(Input.mousePosition);
            if(Vector3.Distance(dragRT.anchoredPosition, newDragPosition) > 100)
            {
                ps.gameObject.SetActive(false);
                dragRT.anchoredPosition = newDragPosition;
            }
            else
            {
                ps.gameObject.SetActive(true);
                dragRT.anchoredPosition = newDragPosition;
            }

        }
    }

    public Vector3 ScreenToCanvasPosition(Vector3 screenPosition)
    {
        var viewportPosition = new Vector3(screenPosition.x / Screen.width,
                                           screenPosition.y / Screen.height,
                                           0);
        return ViewportToCanvasPosition(viewportPosition);
    }

    public Vector3 ViewportToCanvasPosition(Vector3 viewportPosition)
    {
        var canvasRect = canvas.GetComponent<RectTransform>();
        var scale = canvasRect.sizeDelta;
        return Vector3.Scale(viewportPosition, scale);
    }

    public void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {

        // this.gameObject.transform.parent = UICanvas.gameObject.transform;
        // this.rt.sizeDelta
    }
}
