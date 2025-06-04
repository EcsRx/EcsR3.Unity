using EcsR3.Collections.Entities;
using EcsR3.Entities;
using SystemsR3.Events;
using EcsR3.Entities.Accessors;
using EcsR3.Plugins.Views.Systems;
using EcsR3.Plugins.Views.ViewHandlers;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Handlers;
using EcsR3.Unity.MonoBehaviours;
using UnityEngine;

namespace EcsR3.Unity.Systems
{
    public abstract class PooledPrefabViewResolverSystem : PooledViewResolverSystem
    {
        public IUnityInstantiator Instantiator { get; }
        public IEntityCollection EntityCollection { get; }
        
        protected abstract GameObject PrefabTemplate { get; }

        public override IViewHandler CreateViewHandler()
        { return new PrefabViewHandler(Instantiator, PrefabTemplate); }

        protected PooledPrefabViewResolverSystem(IUnityInstantiator instantiator, IEntityCollection entityCollection, IEventSystem eventSystem) : base(eventSystem) 
        {
            Instantiator = instantiator;
            EntityCollection = entityCollection;            
        }

        protected abstract void OnViewAllocated(GameObject view, Entity entity);
        protected abstract void OnViewRecycled(GameObject view, Entity entity);
        
        protected override void OnViewRecycled(IEntityComponentAccessor accessor, object view, Entity entity)
        {
            var gameObject = view as GameObject;
            gameObject.transform.parent = null;

            var entityView = gameObject.GetComponent<EntityView>();
            entityView.Entity = new Entity(-1, 0);

            OnViewRecycled(gameObject, entity);
        }

        protected override void OnViewAllocated(IEntityComponentAccessor accessor, object view, Entity entity)
        {
            var gameObject = view as GameObject;
            var entityView = gameObject.GetComponent<EntityView>();
            entityView.Entity = entity;

            OnViewAllocated(gameObject, entity);
        }
    }
}