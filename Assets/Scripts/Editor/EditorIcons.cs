using UnityEditor;
using UnityEngine;

public class EditorIcons : MonoBehaviour
{
    [MenuItem("Tools/Set Icon")]
    static void SetIcon()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected != null)
        {
            Texture2D icon = EditorGUIUtility.Load("Assets/PathToYourIcon.png") as Texture2D;
            if (icon != null)
            {
                EditorGUIUtility.SetIconForObject(selected, icon);
                Debug.Log("Иконка успешно установлена.");
            }
        }
    }
}