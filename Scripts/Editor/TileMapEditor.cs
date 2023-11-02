using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Herb.Editors
{
    [CustomEditor(typeof(TileMap))]
    public class TileMapEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate Tiles"))
            {
                ((TileMap)target).GenerateTiles();
            }
            if (GUILayout.Button("Clear Childs"))
            {
                ((TileMap)target).DestroyChilds();
            }
        }
    }
}
