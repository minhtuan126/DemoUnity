using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using System.Text;

public class LeaderBoardElement : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _rankText;
    [SerializeField]
    private TextMeshProUGUI _userNameText;
    [SerializeField]
    private TextMeshProUGUI _countText;
    [SerializeField]
    private Image _avatarImage;



    public void Init(UserData user, ToggleGroup toggleGroup)
    {
        int ID_length = 13;
        _countText.SetText(user.totalFish.ToString());
        var idString = user.address;
        if (idString.Length > ID_length)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(idString.Substring(0, 5));
            sb.Append("...");
            sb.Append(idString.Substring(idString.Length - (ID_length - 8)));
            _userNameText.SetText(user.name + '(' + sb + ')');
        }
       
        _avatarImage.sprite = DataReferece.Instance.GetUserAvatarSprite(user.avatar_id);
    }


}
