using SystemsR3.Events;
using EcsR3.Collections.Database;
using EcsR3.Entities;
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
        public IEntityDatabase EntityDatabase { get; }
        public IUnityInstantiator Instantiator { get; }

        protected abstract GameObject PrefabTemplate { get; }

        protected PrefabViewResolverSystem(IEntityDatabase entityDatabase, IEventSystem eventSystem, IUnityInstantiator instantiator) : base(eventSystem)
        {
            EntityDatabase = entityDatabase;
            Instantiator = instantiator;
            ViewHandler = CreateViewHandler();
        }

        protected IViewHandler CreateViewHandler()
        { return new PrefabViewHandler(Instantiator, PrefabTemplate); }

        public override IViewHandler ViewHandler { get; }

        protected override void OnViewCreated(IEntity entity, ViewComponent viewComponent)
        {
            var gameObject = viewComponent.View as GameObject;
            OnViewCreated(entity, gameObject);
        }

        protected abstract void OnViewCreated(IEntity entity, GameObject view);

        public override void Setup(IEntity entity)
        {
            base.Setup(entity);

            var viewComponent = entity.GetComponent<ViewComponent>();
            var gameObject = viewComponent.View as GameObject;
            var entityBinding = gameObject.GetComponent<EntityView>();
            if (entityBinding == null)
            {
                entityBinding = gameObject.AddComponent<EntityView>();
                entityBinding.Entity = entity;

                entityBinding.EntityCollection = EntityDatabase.GetCollectionFor(entity);
            }

            if (viewComponent.DestroyWithView)
            {
                gameObject.OnDestroyAsObservable()
                    .Subscribe(x => entityBinding.EntityCollection.RemoveEntity(entity.Id))
                    .AddTo(gameObject);
            }
        }
    }
}