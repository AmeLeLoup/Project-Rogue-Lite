using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(RoomGenerator))]
    public class RoomGenerator_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
        
            RoomGenerator generator = (RoomGenerator)target;
            //base.OnInspectorGUI();
            DrawDefaultInspector();
        
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate"))
            {
                generator.RestartGeneration();
            }

            if (GUILayout.Button("Clean"))
            {
                generator.StopGeneration();
            }
        
        }
    }
}
