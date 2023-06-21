using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Popup
{
    public class LoadingOpenEgg : PopupBase
    {
        public RectTransform loadingImage;

        private const float _rotateTime = 11f;
        private const float _rotateUpdate = 0.08f;
        private float _currentRotateUpdate = 0f;
        private LTDescr _fadingLTDescr;

        public override void OnShow(PopupInputData popupData)
        {
            base.OnShow(popupData);
            _type = PopupType.Loading;
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

}
