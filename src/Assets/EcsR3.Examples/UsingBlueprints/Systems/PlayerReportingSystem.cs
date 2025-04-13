using EcsR3.Entities;
using EcsR3.Examples.UsingBlueprints.Components;
using EcsR3.Examples.UsingBlueprints.Groups;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using UnityEngine;

namespace EcsR3.Examples.UsingBlueprints.Systems
{
    public class PlayerReportingSystem : ISetupSystem
    {
        public IGroup Group => new PlayerGroup();

        public void Setup(IEntity entity)
        {
            var nameComponent = entity.GetComponent<HasName>();
            var healthComponent = entity.GetComponent<WithHealthComponent>();
            var message = $"{nameComponent.Name} created with {healthComponent.CurrentHealth}/{healthComponent.MaxHealth}";

            Debug.Log(message);
        }
    }
}