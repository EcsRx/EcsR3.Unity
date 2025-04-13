using EcsR3.UnityEditor.Editor.EditorInputs.Basic;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyFloatEditorInput : ReactivePropertyEditorInput<float>
    {
        protected override float CreateTypeUI(string label, float value)
        { return FloatEditorInput.TypeUI(label, value); }
    }
}