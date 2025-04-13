using System;
using EcsR3.Components;

namespace EcsR3.UnityEditor.Events
{
    public class ComponentEvent : EventArgs
    {
        public IComponent Component { get; set; }

        public ComponentEvent(IComponent component)
        {
            Component = component;
        }
    }

    public delegate void ComponentEventHandler(object sender, ComponentEvent eventArgs);
}