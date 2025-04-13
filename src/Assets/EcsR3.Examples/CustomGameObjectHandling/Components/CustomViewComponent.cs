using EcsR3.Components;
using UnityEngine;

namespace EcsR3.Examples.CustomGameObjectHandling.Components
{
    public class CustomViewComponent : IComponent
    {
        public GameObject CustomView { get; set; }
    }
}