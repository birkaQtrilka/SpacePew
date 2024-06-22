using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformRandomizer : EditorWindow
{
    Transform _selectedTransform;

    [MenuItem("Window/TransformRandomizer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TransformRandomizer>("TransformRandomizer");
    }

    void OnGUI()
    {
        GUILayout.Label("SceneObject");

        _selectedTransform = (Transform)EditorGUILayout.ObjectField(_selectedTransform, typeof(Transform), true);

        GUILayout.Label("Select objects in the scene and press button to randomize their rotation");

        if(GUILayout.Button(new GUIContent("Randomize Rotation")))
        {
            //_selectedTransform.rotation = Random.rotation;
            foreach (Transform transform in Selection.transforms)
            {
                transform.rotation = Random.rotation;
            }
        }
    }

}
