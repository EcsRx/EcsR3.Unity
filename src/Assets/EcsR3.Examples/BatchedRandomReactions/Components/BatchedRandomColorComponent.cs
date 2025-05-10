using EcsR3.Components;
using UnityEngine;

namespace EcsR3.Examples.BatchedRandomReactions.Components
{
    public class BatchedRandomColorComponent : IComponent
    {
        public Color Color { get; set; }
        public bool HasChanged { get; set; }
        public float Elapsed { get; set; }
        public float NextChangeIn { get; set; }
    }
}