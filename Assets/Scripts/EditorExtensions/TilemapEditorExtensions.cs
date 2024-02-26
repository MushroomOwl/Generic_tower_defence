using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace TD
{
    [CustomEditor(typeof(Tilemap))]
    public class TilemapEditorExtensions : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Tilemap tilemap = (Tilemap)target;

            if (GUILayout.Button("Compress Bounds"))
            {
                tilemap.CompressBounds();
            }
        }
    }
}
