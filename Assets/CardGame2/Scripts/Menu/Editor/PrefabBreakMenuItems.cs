using UnityEditor;
using UnityEngine;
using System.Linq;

// The purpose of this script is to break the connection of an object with a prefab 
// Blue in Hierarchy => Black in Hierarchy and no connection to its prefab, no Apply, Revert buttons, 
// behaves like a new game object that has not been prefabbed 
public static class PrefabBreakMenuItems 
{
    #region MENU_ITEMS

#if UNITY_EDITOR
    // Breaks the link with the prefab for selected object
    [MenuItem("GameObject/Break Prefab Instance Definitive %&b", false, 29)]
    [MenuItem("CONTEXT/Object/Break Prefab Instance Definitive", false, 301)]
    static void MenuBreakInstanceDefinitive() 
    {    
        GameObject[] breakTargets = Selection.gameObjects;
        Selection.activeGameObject = null;
        BreakInstancesDefinitive(breakTargets);
        Selection.objects = breakTargets;
    }

    // Checks if the object is a part of a prefab (prefab is a parent object)
    [MenuItem("CONTEXT/Object/Break Prefab Instance Definitive", true)]    
    [MenuItem("GameObject/Break Prefab Instance Definitive %&b", true)]
    static bool PrefabCheck() 
    {
        GameObject[] goSelection = Selection.gameObjects;
        return (goSelection.Any(x => PrefabUtility.GetCorrespondingObjectFromSource(x)));
        //return (goSelection.Any(x => PrefabUtility.GetPrefabParent(x)));
    }
#endif

    #endregion

    #region LOGIC

#if UNITY_EDITOR
    // Breaks prefab connection for multiple objects and writes info into Undo.
    public static void BreakInstancesDefinitive(GameObject[] targets)
    {
        Undo.RegisterCompleteObjectUndo(targets, "Breaking multiple prefab instances definitively");

        //Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/dummy.prefab");
        foreach (var target in targets)
        {
            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(target,"Assets/dummy.prefab",InteractionMode.UserAction);
            //PrefabUtility.ReplacePrefab(target, prefab, ReplacePrefabOptions.ConnectToPrefab);
            //PrefabUtility.DisconnectPrefabInstance(target);
        }
        AssetDatabase.DeleteAsset("Assets/dummy.prefab");

        Undo.RecordObjects(targets, "Breaking multiple prefab instances definitively");
    }

    // Breaks prefab connection for one object and writes info into Undo. 
    public static void BreakInstanceDefinitive(GameObject target) 
    {
        Undo.RegisterCompleteObjectUndo(target, "Breaking single prefab instance definitively");

        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(target,"Assets/dummy.prefab",InteractionMode.UserAction);
        //Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/dummy.prefab");

        //PrefabUtility.ReplacePrefab(target, prefab, ReplacePrefabOptions.ConnectToPrefab);
        //PrefabUtility.DisconnectPrefabInstance(target);

        AssetDatabase.DeleteAsset("Assets/dummy.prefab");
    }
#endif

    #endregion
}