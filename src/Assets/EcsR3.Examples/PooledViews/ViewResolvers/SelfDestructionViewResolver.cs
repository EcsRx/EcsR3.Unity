using EcsR3.Collections.Entity;
using SystemsR3.Events;
using EcsR3.Entities;
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

        public SelfDestructionViewResolver(IUnityInstantiator instantiator, IEntityCollection entityCollection, IEventSystem eventSystem)
            : base(instantiator, entityCollection, eventSystem)
        {}

        protected override GameObject PrefabTemplate { get; } = Resources.Load("PooledPrefab") as GameObject;
        
        protected override void OnViewAllocated(GameObject view, IEntity entity)
        {
            view.name = $"pooled-active-{entity.Id}";
            
            var selfDestructComponent = entity.GetComponent<SelfDestructComponent>();
            view.transform.position = selfDestructComponent.StartingPosition;
        }

        protected override void OnViewRecycled(GameObject view, IEntity entity)
        {
            view.name = "pooled-inactive";
        }
    }
}