using System;
using EcsR3.UnityEditor.Editor.EditorInputs.Basic;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public class ReactivePropertyEnumEditorInput : ReactivePropertyEditorInput<Enum>
    {       
        protected override Enum CreateTypeUI(string label, Enum value)
        { return EnumEditorInput.TypeUI(label, value); }
    }
}