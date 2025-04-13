﻿using System;
using EcsR3.Collections.Database;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using R3;

namespace EcsR3.Examples.PooledViews.Systems
{
    public class SelfDestructionSystem : IReactToEntitySystem
    {
        public IGroup Group => new Group(typeof(SelfDestructComponent), typeof(ViewComponent));
        private readonly IEntityCollection _defaultCollection;

        public SelfDestructionSystem(IEntityDatabase entityDatabase)
        { _defaultCollection = entityDatabase.GetCollection(); }

        public Observable<IEntity> ReactToEntity(IEntity entity)
        {
            var selfDestructComponent = entity.GetComponent<SelfDestructComponent>();
            return Observable.Interval(TimeSpan.FromSeconds(selfDestructComponent.Lifetime)).Take(1).Select(x => entity);
        }

        public void Process(IEntity entity)
        { _defaultCollection.RemoveEntity(entity.Id); }
    }
}