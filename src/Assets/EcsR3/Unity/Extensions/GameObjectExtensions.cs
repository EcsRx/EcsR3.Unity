using System;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.Plugins.Views.Components;
using UnityEngine;

namespace EcsR3.Unity.Extensions
{
    public static class GameObjectExtensions
    {
        public static void LinkEntity(this GameObject gameObject, Entity entity, IEntityComponentAccessor accessor)
        {
            if(gameObject.GetComponent<EntityView>())
            { throw new Exception("GameObject already has an EntityView monobehaviour applied"); }

            if (!accessor.HasComponent<ViewComponent>(entity))
            { accessor.CreateComponent<ViewComponent>(entity); }

            var entityViewMb = gameObject.AddComponent<EntityView>();
            entityViewMb.Entity = entity;
            
            var viewComponent = accessor.GetComponent<ViewComponent>(entity);
            viewComponent.View = gameObject;
        }
    }
}