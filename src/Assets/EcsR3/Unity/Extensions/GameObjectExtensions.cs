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
        /// <summary>
        /// Automatically links a game object to an entity via the ViewComponent
        /// </summary>
        /// <param name="gameObject">The game object to link</param>
        /// <param name="entity">The entity to link to</param>
        /// <param name="accessor">The component accessor</param>
        /// <param name="recreateViewComponent">If true removes existing view component, then re-adds it to ensure group join/leave triggers occur</param>
        /// <exception cref="Exception">Thrown if game object is already linked to another entity</exception>
        /// <remarks>A GO can only be linked this way to one entity, and if you want pooling scenarios you should not recreateComponent as that would re-trigger setup systems etc</remarks>
        public static void LinkEntity(this GameObject gameObject, Entity entity, IEntityComponentAccessor accessor, bool recreateViewComponent = false)
        {
            if(gameObject.GetComponent<EntityView>())
            { throw new Exception("GameObject already has an EntityView monobehaviour applied"); }

            if (!accessor.HasComponent<ViewComponent>(entity))
            { accessor.AddComponent(entity, new ViewComponent { View = gameObject }); }
            else
            {
                if (recreateViewComponent)
                {
                    accessor.RemoveComponent<ViewComponent>(entity);
                    accessor.AddComponents(entity, new ViewComponent { View = gameObject });
                }
                else
                {
                    var viewComponent = accessor.GetComponent<ViewComponent>(entity);
                    viewComponent.View = viewComponent.View;
                }
            }
            
            var entityViewMb = gameObject.AddComponent<EntityView>();
            entityViewMb.Entity = entity;
            entityViewMb.EntityComponentAccessor = accessor;
        }
    }
}