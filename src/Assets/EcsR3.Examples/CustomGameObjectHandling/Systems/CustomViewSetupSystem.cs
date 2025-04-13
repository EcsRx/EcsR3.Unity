using EcsR3.Entities;
using EcsR3.Examples.CustomGameObjectHandling.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using UnityEngine;

namespace EcsR3.Examples.CustomGameObjectHandling.Systems
{
    public class CustomViewSetupSystem : ISetupSystem
    {
        public IGroup Group { get { return new Group().WithComponent<CustomViewComponent>();} }
        
        public void Setup(IEntity entity)
        {
            var viewComponent = entity.GetComponent<CustomViewComponent>();
            viewComponent.CustomView = GameObject.CreatePrimitive(PrimitiveType.Cube);
            viewComponent.CustomView.name = $"entity-{entity.Id}";
            var rigidBody = viewComponent.CustomView.AddComponent<Rigidbody>();
            rigidBody.freezeRotation = true;
        }
    }
}