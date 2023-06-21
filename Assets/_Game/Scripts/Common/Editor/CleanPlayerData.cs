using UnityEditor;
using UnityEngine;

public class CleanPlayerData : EditorWindow
{
    [MenuItem("Helper/Clean PlayerPrefs")]
    static void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleaned al player prefs data");
    }

}
