using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Loading : SingletonPersistent<Loading>
{

    public CanvasGroup canvasGroup;
    public RectTransform loadingImage;

    public float fadeTime = 0.2f;
    private const float _rotateTime = 11f;
    private const float _rotateUpdate = 0.08f;
    private float _currentRotateUpdate = 0f;
    private LTDescr _fadingLTDescr;
    public float FadeIn()
    {
        canvasGroup.alpha = 0;
        Fade(1);
        return fadeTime;
    }


    public float FadeOut()
    {
        canvasGroup.alpha = 1;
        Fade(0);
        return fadeTime;
    }

    private void Fade(float targetAlpha)
    {
        if (_fadingLTDescr != null)// && _fadingLTDescr.passed)
            LeanTween.cancel(_fadingLTDescr.uniqueId);

        _fadingLTDescr = LeanTween.alphaCanvas(canvasGroup, targetAlpha, fadeTime);
        var fading = targetAlpha != 0;
        canvasGroup.interactable = fading;
        canvasGroup.blocksRaycasts = fading;
        enabled = fading;
    }

    public void UpdateStatus(float percent)
    {
        // int percentInt = (int)(percent * 100);
        // progressText.SetText(string.Format("{0}%", percentInt));
    }

    private void Update()
    {
        if (loadingImage != null)
        {
            _currentRotateUpdate += Time.unscaledDeltaTime;
            if (_currentRotateUpdate > _rotateUpdate)
            {
                _currentRotateUpdate = 0;
                loadingImage.Rotate(Vector3.forward, -360 / _rotateTime);
            }
        }

    }
}
