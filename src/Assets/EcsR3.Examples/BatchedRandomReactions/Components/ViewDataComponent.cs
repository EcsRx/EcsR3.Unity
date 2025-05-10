using EcsR3.Components;
using UnityEngine;

namespace EcsR3.Examples.BatchedRandomReactions.Components
{
    public class ViewDataComponent : IComponent
    {
        public Material Material { get; set; }
    }
}