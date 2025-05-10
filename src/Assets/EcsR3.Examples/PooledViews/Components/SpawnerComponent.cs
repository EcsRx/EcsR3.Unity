using EcsR3.Components;

namespace EcsR3.Examples.PooledViews.Components
{
    public class SpawnerComponent : IComponent
    {
        public float SpawnRate { get; set; } = 0.5f;
    }
}