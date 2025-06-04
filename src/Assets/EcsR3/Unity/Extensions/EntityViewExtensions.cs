using System.Collections.Generic;
using EcsR3.Components;
using EcsR3.Extensions;
using EcsR3.Unity.MonoBehaviours;

namespace EcsR3.Unity.Extensions
{
    public static class EntityViewExtensions
    {
        public static T GetEcsComponent<T>(this EntityView entityView) where T : IComponent
        { return entityView.EntityComponentAccessor.GetComponent<T>(entityView.Entity); }
                
        public static IEnumerable<IComponent> GetEcsComponents(this EntityView entityView)
        { return entityView.EntityComponentAccessor.GetComponents(entityView.Entity); }
        
        public static bool HasEcsComponent<T>(this EntityView entityView) where T : IComponent
        { return entityView.EntityComponentAccessor.HasComponent<T>(entityView.Entity); }
        
        public static void RemoveEcsComponent<T>(this EntityView entityView) where T : IComponent
        { entityView.EntityComponentAccessor.RemoveComponent<T>(entityView.Entity); }
        
        public static void AddEcsComponent<T>(this EntityView entityView) where T : IComponent, new()
        { entityView.EntityComponentAccessor.CreateComponent<T>(entityView.Entity); }
        
        public static void AddEcsComponent(this EntityView entityView, IComponent component)
        { entityView.EntityComponentAccessor.AddComponent(entityView.Entity, component); }

    }
}