using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Unity
{
    public class GameObjectEditorInput : SimpleEditorInput<GameObject>
    {
        public static GameObject TypeUI(string label, GameObject value)
        { return (GameObject)EditorGUILayout.ObjectField(label, value, typeof(GameObject), true); }
        
        protected override GameObject CreateTypeUI(string label, GameObject value)
        { return TypeUI(label, value); }
    }
}
