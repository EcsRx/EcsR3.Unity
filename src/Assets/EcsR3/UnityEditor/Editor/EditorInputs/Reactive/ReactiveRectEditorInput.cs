using EcsR3.UnityEditor.Editor.EditorInputs.Unity;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyRectEditorInput : ReactivePropertyEditorInput<Rect>
    {
        protected override Rect CreateTypeUI(string label, Rect value)
        { return RectEditorInput.TypeUI(label, value); }
    }
}