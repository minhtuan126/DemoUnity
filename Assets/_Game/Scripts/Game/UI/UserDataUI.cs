using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UserDataUI : MonoBehaviour
{
    private const int ID_LENGTH = 13;
    [SerializeField]
    private Image _userAvatar;
    [SerializeField]
    private TextMeshProUGUI _playerIDText;
    [SerializeField]
    private TextMeshProUGUI _walletIDText;
    // Start is called before the first frame update
    void Start()
    {
        var userData = GameManager.UserData;
        OnUpdateUserData(userData);
        EventManager.AddListener<UserData>(UserDataEvent.UpdateUserInfo, OnUpdateUserData);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UserData>(UserDataEvent.UpdateUserInfo, OnUpdateUserData);
    }

    private void OnUpdateUserData(UserData userData)
    {
        _userAvatar.sprite = userData.avatar;
        _playerIDText.SetText(userData.name);
        var idString = userData.address;
        if (idString.Length > ID_LENGTH)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(idString.Substring(0, 5));
            sb.Append("...");
            sb.Append(idString.Substring(idString.Length - (ID_LENGTH - 8)));
            _walletIDText.SetText(sb.ToString());
        }

    }

}
