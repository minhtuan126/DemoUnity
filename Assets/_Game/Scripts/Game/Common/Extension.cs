using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class Extension
{
    // Start is called before the first frame update
    public static T GetOrAddComponent<T>(this GameObject gObj) where T : Component
    {
        T t = gObj.GetComponent<T>();
        if (t == null)
        {
            t = gObj.AddComponent<T>();
        }
        return t;
    }

    public static void SetAppear(this CanvasGroup canvasGroup, bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    public static void SetActive(this Component component, bool isActive)
    {
        component.gameObject.SetActive(isActive);
    }

    public static string GetJsonFromDictionary(Dictionary<string, string> dic)
    {
        List<string> listKeyValue = new List<string>();

        string json = "{";
        foreach (KeyValuePair<string, string> element in dic)
        {
            listKeyValue.Add($"\"{element.Key}\":\"{element.Value}\"");
        }
        json += String.Join(",", listKeyValue);
        json += "}";
        return json;
    }

    public static string GetPUTFormDataFromDictionary(Dictionary<string, string> dic)
    {
        List<string> listKeyValue = new List<string>();

        string json = "";
        foreach (KeyValuePair<string, string> element in dic)
        {
            listKeyValue.Add($"{element.Key}={element.Value}");
        }
        json += String.Join("&", listKeyValue);
        json += "";
        return json;
    }

    const int k_ADay = 24 * 60 * 60;
    public static string ConvertSecondThanMore(double seconds)
    {
        int days = (int)(seconds / k_ADay);
        string result = "";
        if (days >= 1)
        {
            result = days + (days > 1 ? " Days" : " Day");
            double remainTime = seconds - (days * k_ADay);
            if (remainTime > 0)
            {
                string remainTimeStr = ConvertTimeToString((long)(remainTime));
                result += " " + remainTimeStr;
            }
        }
        else
        {
            result = ConvertTimeToString((long)(seconds));
        }
        return result;
    }

    public static string ConvertTimeToString(long time, bool space = false)
    {
        if (time < 0)
        {
            time = 0;
        }
        long s = time % 60;
        long remainS = time / 60;
        long m = remainS % 60;
        long remainH = remainS / 60;
        return space ? (remainH > 9 ? "" + remainH : "0" + remainH) + " : " + (m > 9 ? "" + m : "0" + m) + " : " + (s > 9 ? "" + s : "0" + s) : (remainH > 9 ? "" + remainH : "0" + remainH) + ":" + (m > 9 ? "" + m : "0" + m) + ":" + (s > 9 ? "" + s : "0" + s);
    }

    public static void TweenText(Text textToTween, float from = 0, float to = 100, float duration = 1f, Action callback = null)
    {
        LeanTween.value(textToTween.gameObject, from, to, duration)
        .setOnUpdate((float value)=>
        {
            textToTween.text = value.ToString();
        })
        .setOnComplete(()=>
        {
            callback.Invoke();
        });
    }

    public static void TweenTMP(TMPro.TextMeshProUGUI textToTween, float from = 0, float to = 100, float duration = 1f, Action callback = null)
    {
        LeanTween.value(textToTween.gameObject, from, to, duration)
        .setOnUpdate((float value)=>
        {
            textToTween.text = value.ToString("F0");
        })
        .setOnComplete(()=>
        {
            textToTween.text = to.ToString();
        });
    }
}
