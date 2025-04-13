using EcsR3.UnityEditor.Editor.EditorInputs.Basic;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyBoolEditorInput : ReactivePropertyEditorInput<bool>
    {
        protected override bool CreateTypeUI(string label, bool value)
        { return BoolEditorInput.TypeUI(label, value); }
    }
}