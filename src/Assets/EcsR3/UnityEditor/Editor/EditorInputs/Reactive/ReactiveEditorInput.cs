using System;
using R3;

namespace EcsR3.UnityEditor.Editor.EditorInputs.Reactive
{
    public abstract class ReactivePropertyEditorInput<T> : IEditorInput
    {
        private static readonly Type ReactivePropertyType = typeof(ReactiveProperty<T>);
        
        public  bool HandlesType(Type type)
        { return ReactivePropertyType.IsAssignableFrom(type); }
        
        public ReactiveProperty<T> GetValue(object value)
        { return (ReactiveProperty<T>)value; }

        public UIStateChange CreateUI(string label, object value)
        {
            var reactiveProperty = GetValue(value);
            var originalValue = reactiveProperty.Value;
            var newValue = CreateTypeUI(label, originalValue);
            
            if(newValue.Equals(originalValue))
            { return UIStateChange.NoChange; }

            reactiveProperty.Value = newValue;
            return new UIStateChange {HasChanged = true};
        }

        protected abstract T CreateTypeUI(string label, T value);
    }
}