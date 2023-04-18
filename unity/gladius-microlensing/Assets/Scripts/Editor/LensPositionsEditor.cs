using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LensPositions))]
public class LensPositionsEditor : Editor
{
    private LensPositions positions;

    private void OnEnable()
    {
        positions = target as LensPositions;
    }

    private void OnDisable()
    {
        positions = null;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Randomize"))
        {
            positions.Randomize();
        }

        if (GUI.changed)
        {
            positions.UpdateTexture();
        }
    }
}
