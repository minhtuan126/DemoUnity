using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Helper class for instantiating ScriptableObjects.
/// </summary>
public class ScriptableObjectFactory
{
    [MenuItem("Project/Create/ScriptableObject")]
    [MenuItem("Assets/Create/ScriptableObject")]
    public static void Create()
    {
        var assembly = GetAssembly();

        // Get all classes derived from ScriptableObject
        var allScriptableObjects = (from t in assembly.GetTypes()
                                    where t.IsSubclassOf(typeof(ScriptableObject))
                                    select t).ToArray();

        // Show the selection window.
        var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
        window.ShowPopup();

        window.Types = allScriptableObjects;
    }

    /// <summary>
    /// Returns the assembly that contains the script code for this project (currently hard coded)
    /// </summary>
    private static Assembly GetAssembly()
    {
        return Assembly.Load(new AssemblyName("Assembly-CSharp"));
    }

    // [MenuItem("Project/Helper/AbilityModel")]
    // [MenuItem("Assets/Helper/AbilityModel")]
    // public static void CreateAbility()
    // {
    //     var assembly = GetAssembly();

    //     // Get all classes derived from ScriptableObject
    //     var allScriptableObjects = (from t in assembly.GetTypes()
    //                                 where t.IsSubclassOf(typeof(AbilityBuilder))
    //                                 select t).ToArray();

    //     // Show the selection window.
    //     var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new Ability", true);
    //     window.ShowPopup();

    //     window.Types = allScriptableObjects;
    // }

}
