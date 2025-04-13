using EcsR3.Plugins.Views.Components;
using UnityEngine;

namespace EcsR3.Unity.Extensions
{
    public static class ViewComponentExtensions
    {
        public static void DestroyView(this ViewComponent viewComponent, float delay = 0.0f)
        {
            Object.Destroy((GameObject)viewComponent.View, delay);
        }
    }
}