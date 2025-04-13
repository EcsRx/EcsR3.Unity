using EcsR3.Components;

namespace EcsR3.Examples.UsingBlueprints.Components
{
    public class WithHealthComponent : IComponent
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
    }
}