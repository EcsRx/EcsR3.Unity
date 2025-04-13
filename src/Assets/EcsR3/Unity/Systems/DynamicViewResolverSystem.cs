using SystemsR3.Events;
using EcsR3.Unity.Dependencies;
using EcsR3.Collections.Database;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using R3;
using R3.Triggers;
using UnityEngine;

namespace EcsR3.Unity.Systems
{
    public abstract class DynamicViewResolverSystem : ISetupSystem, ITeardownSystem
    {
        public IEventSystem EventSystem { get; }
        public IEntityDatabase EntityDatabase { get; }
        public IUnityInstantiator Instantiator { get; }

        public abstract IGroup Group { get; }

        protected DynamicViewResolverSystem(IEventSystem eventSystem, IEntityDatabase entityDatabase, IUnityInstantiator instantiator)
        {
            EventSystem = eventSystem;
            EntityDatabase = entityDatabase;
            Instantiator = instantiator;
        }

        public abstract GameObject CreateView(IEntity entity);
        public abstract void DestroyView(IEntity entity, GameObject view);
        
        public void Setup(IEntity entity)
        {
            var viewComponent = entity.GetComponent<ViewComponent>();
            if (viewComponent.View != null) { return; }
            
            var viewGameObject = CreateView(entity);
            viewComponent.View = viewGameObject;
            
            var entityBinding = viewGameObject.GetComponent<EntityView>();
            if (entityBinding == null)
            {
                entityBinding = viewGameObject.AddComponent<EntityView>();
                entityBinding.Entity = entity;
                entityBinding.EntityCollection = EntityDatabase.GetCollectionFor(entity);
            }

            if (viewComponent.DestroyWithView)
            {
                viewGameObject.OnDestroyAsObservable()
                    .Subscribe(x => entityBinding.EntityCollection.RemoveEntity(entity.Id))
                    .AddTo(viewGameObject);
            }
        }
        
        public void Teardown(IEntity entity)
        {
            var component = entity.GetComponent<ViewComponent>();
            DestroyView(entity, component.View as GameObject);
        }
    }
}