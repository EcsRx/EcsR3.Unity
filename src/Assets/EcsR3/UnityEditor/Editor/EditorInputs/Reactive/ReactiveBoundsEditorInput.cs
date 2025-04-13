using EcsR3.UnityEditor.Editor.EditorInputs.Unity;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyBoundsEditorInput : ReactivePropertyEditorInput<Bounds>
    {
        protected override Bounds CreateTypeUI(string label, Bounds value)
        { return BoundsEditorInput.TypeUI(label, value); }
    }
}