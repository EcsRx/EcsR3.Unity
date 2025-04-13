using System;
using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Examples.PooledViews.Blueprints;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using R3;
using UnityEngine;

namespace EcsR3.Examples.PooledViews.Systems
{
    public class SpawnSystem : IReactToEntitySystem
    {
        private readonly IEntityCollection _defaultCollection;

        public IGroup Group => new Group(typeof(SpawnerComponent), typeof(ViewComponent));

        public SpawnSystem(IEntityDatabase entityDatabase)
        { _defaultCollection = entityDatabase.GetCollection(); }

        public Observable<IEntity> ReactToEntity(IEntity entity)
        {
            var spawnComponent = entity.GetComponent<SpawnerComponent>();
            return Observable.Interval(TimeSpan.FromSeconds(spawnComponent.SpawnRate)).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var viewComponent = entity.GetComponent<ViewComponent>();
            var view = viewComponent.View as GameObject;
            var blueprint = new SelfDestructBlueprint(view.transform.position);
            _defaultCollection.CreateEntity(blueprint);
        }
    }
}