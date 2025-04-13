using System;
using EcsR3.Collections;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.Plugins.Views.Components;
using UnityEngine;

namespace EcsR3.Unity.Extensions
{
    public static class GameObjectExtensions
    {
        public static void LinkEntity(this GameObject gameObject, IEntity entity, IEntityCollection withinPool)
        {
            if(gameObject.GetComponent<EntityView>())
            { throw new Exception("GameObject already has an EntityView monobehaviour applied"); }

            if (!entity.HasComponent<ViewComponent>())
            { entity.AddComponent<ViewComponent>(); }

            var entityViewMb = gameObject.AddComponent<EntityView>();
            entityViewMb.Entity = entity;
            entityViewMb.EntityCollection = withinPool;
            
            var viewComponent = entity.GetComponent<ViewComponent>();
            viewComponent.View = gameObject;
        }
    }
}