using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static float oldPearl;
    public static float oldEggPiece;
    public static UserData UserData { get; set; }

    public static void ResetData()
    {
        UserData = null;

    }
    public static void InitUserData(UserData userData)
    {
        UserData = userData;
        InitRuntimeData(UserData);
    }

    private static void InitRuntimeData(UserData userData)
    {
        // for (int i = 0; i < userData.eggList.Count; i++)
        // {
        //     userData.eggList[i].avatar = GameInitFollow.Instance.GetEggSprite(userData.eggList[i].rarity);
        // }

        // if (userData.MainFish == null)
        // {
        //     UpdateUserData();
        // }
    }

    public static void UpdateUserSetting()
    {
        APIManager.Instance.UpdateSoundSetting(UserData).Forget();
        EventManager.Invoke<UserData>(UserDataEvent.UpdatedUserSetting, GameManager.UserData);
    }

    public static void UpdateUserInfo()
    {
        APIManager.Instance.UpdateUserInfo(UserData).Forget();
        EventManager.Invoke<UserData>(UserDataEvent.UpdateUserInfo, GameManager.UserData);
    }
}

public enum UserDataEvent
{
    UpdateUserInfo,
    UpdatedUserAvatar,
    UpdatedUserSetting,
}
