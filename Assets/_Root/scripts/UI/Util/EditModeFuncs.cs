#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Hullbreakers
{
    [CustomEditor(typeof(SnakeSpawner))]
    public class EditModeFunctions : Editor
    {
    
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SnakeSpawner spawner = (SnakeSpawner)target;

            if (GUILayout.Button("Generate"))
            {
                spawner.Generate();
            }
        }
    }
}
#endif