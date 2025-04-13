using EcsR3.Components;
using UnityEngine;

namespace EcsR3.Examples.SimpleMovement.Components
{
    public class CameraFollowsComponent : IComponent
    {
        public Camera Camera { get; set; }         
    }
}