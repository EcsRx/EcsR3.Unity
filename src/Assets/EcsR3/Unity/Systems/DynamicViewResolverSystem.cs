using EcsR3.Collections.Entities;
using EcsR3.Entities;
using SystemsR3.Events;
using EcsR3.Unity.Dependencies;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using R3;
using R3.Triggers;
using UnityEngine;

namespace EcsR3.Unity.Systems
{
    public abstract class DynamicViewResolverSystem : ISetupSystem, ITeardownSystem
    {
        public IEventSystem EventSystem { get; }
        public IEntityCollection EntityCollection { get; }
        public IUnityInstantiator Instantiator { get; }

        public abstract IGroup Group { get; }

        protected DynamicViewResolverSystem(IEventSystem eventSystem, IEntityCollection entityCollection, IUnityInstantiator instantiator)
        {
            EventSystem = eventSystem;
            EntityCollection = entityCollection;
            Instantiator = instantiator;
        }

        public abstract GameObject CreateView(Entity entity);
        public abstract void DestroyView(Entity entity, GameObject view);
        
        public void Setup(IEntityComponentAccessor accessor, Entity entity)
        {
            var viewComponent = accessor.GetComponent<ViewComponent>(entity);
            if (viewComponent.View != null) { return; }
            
            var viewGameObject = CreateView(entity);
            viewComponent.View = viewGameObject;
            
            var entityBinding = viewGameObject.GetComponent<EntityView>();
            if (entityBinding == null)
            {
                entityBinding = viewGameObject.AddComponent<EntityView>();
                entityBinding.Entity = entity;
            }

            if (viewComponent.DestroyWithView)
            {
                viewGameObject.OnDestroyAsObservable()
                    .Subscribe(x => EntityCollection.Remove(entity))
                    .AddTo(viewGameObject);
            }
        }
        
        public void Teardown(IEntityComponentAccessor accessor, Entity entity)
        {
            var component = accessor.GetComponent<ViewComponent>(entity);
            DestroyView(entity, component.View as GameObject);
        }
    }
}