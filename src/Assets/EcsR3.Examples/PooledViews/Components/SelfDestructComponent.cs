using EcsR3.Components;
using UnityEngine;

namespace EcsR3.Examples.PooledViews.Components
{
    public class SelfDestructComponent : IComponent
    {
        public Vector3 StartingPosition { get; set; }
        public float Lifetime { get; set; }
    }
}