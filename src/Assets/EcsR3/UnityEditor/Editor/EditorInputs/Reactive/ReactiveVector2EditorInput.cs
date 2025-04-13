using EcsR3.UnityEditor.Editor.EditorInputs.Unity;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyVector2EditorInput : ReactivePropertyEditorInput<Vector2>
    {
        protected override Vector2 CreateTypeUI(string label, Vector2 value)
        { return Vector2EditorInput.TypeUI(label, value); }
    }
}