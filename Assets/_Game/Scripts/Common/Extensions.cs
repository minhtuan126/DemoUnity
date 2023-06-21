using System.Collections;
using System.Collections.Generic;
using Popup;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Extensions
{
    #region DateTime
    public static int ToMilisecond(this float second)
    {
        return (int)(second * 1000);
    }

    #endregion

    #region  Game Object

    public static T FindComponentInChildren<T>(this GameObject obj, string name) where T : Component
    {
        var allComponents = obj.transform.GetComponentsInChildren<T>();

        if (allComponents != null)
        {
            foreach (var component in allComponents)
            {
                if (string.Compare(name, component.gameObject.name) == 0)
                    return component;
            }
        }
        return null;
    }


    public static List<T> FindComponentsInChildren<T>(this GameObject obj, string name) where T : Component
    {
        var allComponents = obj.transform.GetComponentsInChildren<T>();

        if (allComponents != null)
        {
            List<T> listComponents = new List<T>();
            foreach (var component in allComponents)
            {
                if (string.Compare(name, component.gameObject.name) == 0)
                {
                    listComponents.Add(component);
                }
            }
            return listComponents;
        }
        return null;
    }

    #endregion

    #region  Game Extension
    public static void EnterMarketPlace() {
        #if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            // Application.OpenURL("https://metamask.app.link/dapp/market.fishnft.sv0.console.papagroup.net");
            Application.OpenURL("https://metamask.app.link/dapp/market.testnet.fishme.io");
        #else
            // Application.OpenURL("https://market.fishnft.sv0.console.papagroup.net");
            Application.OpenURL("https://market.testnet.fishme.io/");
        #endif
    }

    public static void LogOut()
    {
        PlayerPrefs.SetString("Account", "");
        PlayerPrefs.SetString("IsLogout", "1");
        AuthenticationKit.Instance.Disconnect();
        if (PopupManager.Instance)
        {
            LeanTween.delayedCall(0.5f,()=>PopupManager.Instance.OnHideAllPopups());
        }
        //LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Home);
        SceneManager.LoadScene(1);
    }
    #endregion
}
