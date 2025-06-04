using EcsR3.Collections.Entities;
using EcsR3.Entities;
using SystemsR3.Events;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Handlers;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.Plugins.Views.Components;
using EcsR3.Plugins.Views.Systems;
using EcsR3.Plugins.Views.ViewHandlers;
using R3;
using R3.Triggers;
using UnityEngine;

namespace EcsR3.Unity.Systems
{
    public abstract class PrefabViewResolverSystem : ViewResolverSystem
    {
        public IEntityCollection EntityCollection { get; }
        public IUnityInstantiator Instantiator { get; }

        protected abstract GameObject PrefabTemplate { get; }

        protected PrefabViewResolverSystem(IEntityCollection entityCollection, IEventSystem eventSystem, IUnityInstantiator instantiator) : base(eventSystem)
        {
            EntityCollection = entityCollection;
            Instantiator = instantiator;
        }

        public override IViewHandler CreateViewHandler()
        { return new PrefabViewHandler(Instantiator, PrefabTemplate); }

        protected override void OnViewCreated(IEntityComponentAccessor accessor, Entity entity, ViewComponent viewComponent)
        {
            var gameObject = viewComponent.View as GameObject;
            OnViewCreated(accessor, entity, gameObject);
        }

        protected abstract void OnViewCreated(IEntityComponentAccessor accessor, Entity entity, GameObject view);

        public override void Setup(IEntityComponentAccessor accessor, Entity entity)
        {
            base.Setup(accessor, entity);

            var viewComponent = accessor.GetComponent<ViewComponent>(entity);
            var gameObject = viewComponent.View as GameObject;
            var entityBinding = gameObject.GetComponent<EntityView>();
            if (entityBinding == null)
            {
                entityBinding = gameObject.AddComponent<EntityView>();
                entityBinding.Entity = entity;
            }

            if (viewComponent.DestroyWithView)
            {
                gameObject.OnDestroyAsObservable()
                    .Subscribe(x => EntityCollection.Remove(entity))
                    .AddTo(gameObject);
            }
        }
    }
}