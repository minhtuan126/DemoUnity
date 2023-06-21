using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Timers;
using System;

namespace Popup
{
    public abstract class PopupBase : MonoBehaviour//, where T : PopupInputData
    {
        protected static int ShowingBool = Animator.StringToHash("Showing");
        protected static int CloseTrigger = Animator.StringToHash("Close");
        protected static int OpenTrigger = Animator.StringToHash("Open");

        [Header("Base Attributes")]
        [SerializeField]
        protected CanvasGroup _canvasGroup;

        [SerializeField]
        protected Animator _contentAnimator;
        [SerializeField]
        protected RectTransform _content;

        [SerializeField]
        protected float _fadeTime = 0.2f;

        [SerializeField]
        protected Button _backOrCloseButton;
        protected bool _isShowing = false;
        protected PopupType _type = PopupType.Count;

#if UNITY_EDITOR

        [SerializeField]
        [Header("Editor Only")]
        protected bool isShowing_Editor = true;

        private void OnValidate()
        {
            if (Application.isPlaying) return;

            if (_contentAnimator == null)
                _contentAnimator = GetComponentInChildren<Animator>();

            if (_canvasGroup == null)
                _canvasGroup = GetComponentInChildren<CanvasGroup>();

            if (_content == null)
            {
                _content = gameObject.FindComponentInChildren<RectTransform>("Content");
            }

            if (_canvasGroup)
                _canvasGroup.alpha = isShowing_Editor ? 1 : 0;

            if (_content)
                _content.localScale = isShowing_Editor ? Vector3.one : Vector3.zero;
        }

#endif

        [Header("Child Attributes")]
        protected PopupInputData _popupInputData;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (_backOrCloseButton != null)
                _backOrCloseButton.onClick.AddListener(OnHide);
        }

        protected virtual void ResetData()
        {
        }

        protected virtual T GetPopupInputData<T>() where T : PopupInputData
        {
            return _popupInputData as T;
        }

        public virtual void OnShow(PopupInputData popupData)
        {
            ResetData();
            _popupInputData = popupData;
            PlayShow().Forget();
        }


        public virtual void OnHide()
        {
            if (!_isShowing) return;

            PlayHide().Forget();
            PopupManager.Instance.OnHidePopup(this);
        }

        protected virtual async UniTask PlayShow()
        {
            transform.SetAsLastSibling();
            if (!_isShowing)
                await FadeIn();
            _isShowing = true;
            FieldData();
        }

        protected virtual async UniTask PlayHide()
        {
            if (_isShowing)
                await FadeOut();
            _isShowing = false;
        }

        public PopupType GetPopupType()
        {
            return _type;
        }

        protected async virtual UniTask FadeIn()
        {
            _contentAnimator.SetTrigger(OpenTrigger);
            _contentAnimator.SetBool(ShowingBool, true);
            await UniTask.Delay(_fadeTime.ToMilisecond());
        }

        protected async virtual UniTask FadeOut()
        {
            _contentAnimator.SetTrigger(CloseTrigger);
            await UniTask.Delay(_fadeTime.ToMilisecond());
        }

        protected virtual void FieldData()
        {

        }


    }


    public abstract class PopupInputData
    {

    }
}
