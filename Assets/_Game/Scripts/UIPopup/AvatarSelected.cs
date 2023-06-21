using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AvatarSelected : MonoBehaviour
{
    [Serializable]
    public struct ToggleAvatar
    {
        public Toggle avatarToggle;
        public Sprite avatar;
        public int id;
    }

    [SerializeField]
    private Toggle _avatarTogglePrefab;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private ToggleGroup _avatarToggleGroup;

    public UnityEvent<Sprite> OnSelectAvatar;

    [SerializeField]
    private Button _finishedEditAvatar;
    private Sprite _avatarSelected;
    private List<ToggleAvatar> _avatarToggles;

    private void OnValidate()
    {

        if (_avatarToggleGroup == null)
            _avatarToggleGroup = GetComponentInChildren<ToggleGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_avatarToggles == null)
        {
            _avatarToggles = new List<ToggleAvatar>();
            var avatarData = DataReferece.Instance.AvatarDataDict;
            foreach (var key in avatarData.Keys)
            {
                var avatarToggle = Instantiate(_avatarTogglePrefab, _content);
                avatarToggle.group = _avatarToggleGroup;
                var avatarImage = avatarToggle.gameObject.FindComponentInChildren<Image>("Avatar");
                avatarImage.sprite = avatarData[key];

                _avatarToggles.Add(new ToggleAvatar()
                {
                    avatarToggle = avatarToggle,
                    avatar = avatarImage.sprite,
                    id = key
                });
            }
        }

        _finishedEditAvatar.onClick.AddListener(OnFinishedEditAvatar);
        for (int i = 0; i < _avatarToggles.Count; i++)
        {
            var toggle = _avatarToggles[i];
            toggle.avatarToggle.onValueChanged.AddListener((isOn) => OnSelected(isOn, toggle, toggle.id));
        }
    }
    private void OnEnable()
    {
        _avatarSelected = null;
    }

    private void OnFinishedEditAvatar()
    {
        if (OnSelectAvatar != null)
        {
            OnSelectAvatar.Invoke(_avatarSelected);
        }
    }

    private void OnSelected(bool isOn, ToggleAvatar toggle, int id)
    {
        if (isOn)
        {
            _avatarSelected = toggle.avatar;
            GameManager.UserData.avatar_id = id;
        }
    }
}
