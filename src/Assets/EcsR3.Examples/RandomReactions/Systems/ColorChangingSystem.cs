using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using EcsR3.Systems.Reactive;
using SystemsR3.Scheduling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EcsR3.Examples.RandomReactions.Systems
{
    public class ColorChangingSystem : IBasicEntitySystem, ISetupSystem
    {
        private readonly float MaxDelay = 5.0f;
        private readonly float MinDelay = 1.0f;

        public IGroup Group => new Group(typeof(RandomColorComponent));

        public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var randomColorComponent = entityComponentAccessor.GetComponent<RandomColorComponent>(entity);
            randomColorComponent.NextChangeIn = Random.Range(MinDelay, MaxDelay);
        }
        
        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
        {
            var randomColorComponent = entityComponentAccessor.GetComponent<RandomColorComponent>(entity);
            randomColorComponent.Elapsed += Time.deltaTime;

            if (!(randomColorComponent.Elapsed >= randomColorComponent.NextChangeIn))
            { return; }

            randomColorComponent.Elapsed -= randomColorComponent.NextChangeIn;
            randomColorComponent.NextChangeIn = Random.Range(MinDelay, MaxDelay);

            var r = Random.Range(0.0f, 1.0f);
            var g = Random.Range(0.0f, 1.0f);
            var b = Random.Range(0.0f, 1.0f);
            randomColorComponent.Color.Value = new Color(r,g,b);
        }
    }
}