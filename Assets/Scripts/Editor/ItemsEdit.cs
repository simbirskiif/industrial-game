using UnityEditor;
using UnityEngine;

public class ItemsEdit : EditorWindow
{
    MasterManager masterManager;
    [MenuItem("Window/Items Edit")]
    public static void ShowWindow()
    {
        GetWindow<ItemsEdit>("Items Edit");
    }
    private void OnGUI()
    {
        GUILayout.Label("Items", EditorStyles.boldLabel);
        masterManager = (MasterManager)EditorGUILayout.ObjectField("Целевой объект:", masterManager, typeof(MasterManager), true);
        if(masterManager == null){
            return;
        }
    }
}
