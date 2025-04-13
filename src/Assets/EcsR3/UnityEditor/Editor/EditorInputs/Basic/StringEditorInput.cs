using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Basic
{
    public class StringEditorInput : SimpleEditorInput<string>
    {
        public static string TypeUI(string label, string value)
        { return EditorGUILayout.TextField(label, value); }
        
        protected override string CreateTypeUI(string label, string value)
        { return TypeUI(label, value); }
    }
}