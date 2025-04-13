using EcsR3.UnityEditor.Editor.EditorInputs.Basic;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyStringEditorInput : ReactivePropertyEditorInput<string>
    {
        protected override string CreateTypeUI(string label, string value)
        { return StringEditorInput.TypeUI(label, value); }
    }
}