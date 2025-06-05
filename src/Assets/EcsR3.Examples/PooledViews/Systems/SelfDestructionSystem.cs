using System;
using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using R3;

namespace EcsR3.Examples.PooledViews.Systems
{
    public class SelfDestructionSystem : IReactToEntitySystem
    {
        public IGroup Group => new Group(typeof(SelfDestructComponent), typeof(ViewComponent));
        private readonly IEntityCollection EntityCollection;

        public SelfDestructionSystem(IEntityCollection entityCollection)
        { EntityCollection = entityCollection; }

        public Observable<Entity> ReactToEntity(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var selfDestructComponent = entityComponentAccessor.GetComponent<SelfDestructComponent>(entity);
            return Observable.Interval(TimeSpan.FromSeconds(selfDestructComponent.Lifetime)).Take(1).Select(x => entity);
        }

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        { EntityCollection.Remove(entity); }
    }
}