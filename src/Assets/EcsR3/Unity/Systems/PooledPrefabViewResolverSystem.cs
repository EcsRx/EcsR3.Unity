using EcsR3.Collections.Entity;
using SystemsR3.Events;
using EcsR3.Entities;
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

        protected abstract void OnViewAllocated(GameObject view, IEntity entity);
        protected abstract void OnViewRecycled(GameObject view, IEntity entity);

        protected override void OnViewRecycled(object view, IEntity entity)
        {
            var gameObject = view as GameObject;
            gameObject.transform.parent = null;

            var entityView = gameObject.GetComponent<EntityView>();
            entityView.Entity = null;
            entityView.EntityCollection = null;

            OnViewRecycled(gameObject, entity);
        }

        protected override void OnViewAllocated(object view, IEntity entity)
        {
            var gameObject = view as GameObject;
            var entityView = gameObject.GetComponent<EntityView>();
            entityView.Entity = entity;
            entityView.EntityCollection = EntityCollection;

            OnViewAllocated(gameObject, entity);
        }
    }
}