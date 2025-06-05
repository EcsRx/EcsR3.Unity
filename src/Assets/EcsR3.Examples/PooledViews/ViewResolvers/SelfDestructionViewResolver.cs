using EcsR3.Collections.Entities;
using SystemsR3.Events;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using EcsR3.Plugins.Views.Components;
using SystemsR3.Pools.Config;
using UnityEngine;

namespace EcsR3.Examples.PooledViews.ViewResolvers
{
    public class SelfDestructionViewResolver : PooledPrefabViewResolverSystem
    {
        public override IGroup Group { get; } = new Group(typeof(SelfDestructComponent), typeof(ViewComponent));
        public override PoolConfig PoolConfig { get; } = new PoolConfig(10, 5);
        public IEntityComponentAccessor EntityComponentAccessor { get; }

        public SelfDestructionViewResolver(IUnityInstantiator instantiator, IEntityCollection entityCollection, IEventSystem eventSystem, IEntityComponentAccessor entityComponentAccessor)
            : base(instantiator, entityCollection, eventSystem)
        {
            EntityComponentAccessor = entityComponentAccessor;
        }

        protected override GameObject PrefabTemplate { get; } = Resources.Load("PooledPrefab") as GameObject;
        
        protected override void OnViewAllocated(GameObject view, Entity entity)
        {
            view.name = $"pooled-active-{entity.Id}";
            
            var selfDestructComponent = EntityComponentAccessor.GetComponent<SelfDestructComponent>(entity);
            view.transform.position = selfDestructComponent.StartingPosition;
        }

        protected override void OnViewRecycled(GameObject view, Entity entity)
        {
            view.name = "pooled-inactive";
        }
    }
}