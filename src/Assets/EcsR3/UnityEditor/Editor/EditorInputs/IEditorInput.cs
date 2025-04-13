using System;

namespace EcsR3.UnityEditor.Editor.EditorInputs
{
    public interface IEditorInput
    {
        bool HandlesType(Type type);
        UIStateChange CreateUI(string label, object value);
    }
}