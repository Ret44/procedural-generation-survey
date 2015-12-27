using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor {
   public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshGenerator myScript = (MeshGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }
    }
}
