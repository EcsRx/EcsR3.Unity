using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Examples.UsingBlueprints.Components;
using EcsR3.Extensions;

namespace EcsR3.Examples.UsingBlueprints.Blueprints
{
    public class PlayerBlueprint : IBlueprint
    {
        public float DefaultHealth { get; }
        public string Name { get; }

        public PlayerBlueprint(string name, float defaultHealth = 100.0f)
        {
            DefaultHealth = defaultHealth;
            Name = name;
        }

        public void Apply(IEntity entity)
        {
            entity.AddComponents(new HasName { Name = Name }, 
                new WithHealthComponent { CurrentHealth = DefaultHealth, MaxHealth = DefaultHealth});
        }
    }
}