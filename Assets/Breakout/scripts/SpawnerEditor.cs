using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Spawner myScript = (Spawner)target;

        if (GUILayout.Button("NL")) {
            //myScript.NewLine();
        }
    }
}