using System;
using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.PooledViews.Blueprints;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using R3;
using UnityEngine;

namespace EcsR3.Examples.PooledViews.Systems
{
    public class SpawnSystem : IReactToEntitySystem
    {
        private readonly IEntityCollection EntityCollection;

        public IGroup Group => new Group(typeof(SpawnerComponent), typeof(ViewComponent));

        public SpawnSystem(IEntityCollection entityCollection)
        { EntityCollection = entityCollection; }

        public Observable<Entity> ReactToEntity(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var spawnComponent = entityComponentAccessor.GetComponent<SpawnerComponent>(entity);
            return Observable.Interval(TimeSpan.FromSeconds(spawnComponent.SpawnRate)).Select(x => entity);
        }

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
            var view = viewComponent.View as GameObject;
            var blueprint = new SelfDestructBlueprint(view.transform.position);
            EntityCollection.Create(entityComponentAccessor, blueprint);
        }
    }
}