using SystemsR3.Events;
using EcsR3.Collections.Database;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Pooling;
using EcsR3.Plugins.Views.Systems;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Handlers;
using EcsR3.Unity.MonoBehaviours;
using UnityEngine;

namespace EcsR3.Unity.Systems
{
    public abstract class PooledPrefabViewResolverSystem : PooledViewResolverSystem
    {
        public IUnityInstantiator Instantiator { get; }
        public IEntityDatabase EntityDatabase { get; }
        
        protected abstract GameObject PrefabTemplate { get; }
        protected abstract int PoolIncrementSize { get; }

        protected override IViewPool CreateViewPool()
        { return new ViewPool(PoolIncrementSize, new PrefabViewHandler(Instantiator, PrefabTemplate)); }

        protected PooledPrefabViewResolverSystem(IUnityInstantiator instantiator, IEntityDatabase entityDatabase, IEventSystem eventSystem) : base(eventSystem) 
        {
            Instantiator = instantiator;
            EntityDatabase = entityDatabase;            
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
            var entityCollection = EntityDatabase.GetCollectionFor(entity);
            entityView.Entity = entity;
            entityView.EntityCollection = entityCollection;

            OnViewAllocated(gameObject, entity);
        }
    }
}