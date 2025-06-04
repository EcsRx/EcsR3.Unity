using System;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using UnityEngine;

namespace EcsR3.Unity.Extensions
{
    public static class IEntityExtensions
    {
        public static T GetUnityComponent<T>(this IEntityComponentAccessor entityComponentAccessor, Entity entity) where T : Component
        {
            if(!entityComponentAccessor.HasComponent<ViewComponent>(entity))
            { return null; }

            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);

            if(viewComponent.View == null)
            { return null; }

            var castView = (GameObject) viewComponent.View;
            return castView.GetComponent<T>();
        }

        public static T AddUnityComponent<T>(this IEntityComponentAccessor entityComponentAccessor, Entity entity) where T : Component
        {
            if (!entityComponentAccessor.HasComponent<ViewComponent>(entity))
            { throw new Exception("Entity has no ViewComponent, ensure a valid ViewComponent is applied with an active View"); }

            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);

            if (viewComponent.View == null)
            { throw new Exception("Entity's ViewComponent has no assigned View, GameObject has been applied to the View"); }

            var castView = (GameObject) viewComponent.View;
            return castView.AddComponent<T>();
        }

        public static GameObject GetGameObject(this IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
            return viewComponent.View as GameObject;
        }
    }
}