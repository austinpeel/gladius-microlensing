using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteCollection))]
public class SpriteCollectionEditor : Editor
{
    private SpriteCollection collection;
    private int selectedIndex;

    private void OnEnable()
    {
        collection = target as SpriteCollection;

        // Synchronize the editor's value for dropdown
        selectedIndex = collection.currentSpriteIndex + 1;
    }

    private void OnDisable()
    {
        collection = null;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (collection.sprites == null) return;

        string[] options = new string[collection.sprites.Length + 1];
        options[0] = "None";
        for (int i = 1; i < options.Length; i++)
        {
            options[i] = "Sprite " + i;
        }

        // Create a dropdown for choosing which sprite to display
        selectedIndex = EditorGUILayout.Popup("Show Sprite", selectedIndex, options);

        if (GUI.changed)
        {
            collection.ShowSprite(selectedIndex - 1);
            Debug.Log(((RectTransform)collection.transform).sizeDelta);
        }
    }
}
