using EcsR3.UnityEditor.Editor.EditorInputs.Basic;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyIntEditorInput : ReactivePropertyEditorInput<int>
    {
        protected override int CreateTypeUI(string label, int value)
        { return IntEditorInput.TypeUI(label, value); }
    }
}