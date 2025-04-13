using System;
using System.Collections.Generic;
using System.Linq;
using EcsR3.Components;

namespace EcsR3.UnityEditor.Editor.Helpers
{
    public static class ComponentLookup
    {
        public static IEnumerable<Type> AllComponents { get; private set; }

        static ComponentLookup()
        {
            AllComponents = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(IsComponent)
                .ToArray();
        }

        private static bool IsComponent(Type type)
        {
            return typeof(IComponent).IsAssignableFrom(type) &&
                   type.IsClass;

        }
    }
}