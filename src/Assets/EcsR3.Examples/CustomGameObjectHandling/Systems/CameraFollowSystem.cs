using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.CustomGameObjectHandling.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using EcsR3.Systems.Reactive;
using SystemsR3.Scheduling;
using UnityEngine;

namespace EcsR3.Examples.CustomGameObjectHandling.Systems
{
    public class CameraFollowSystem : ISetupSystem, IBasicEntitySystem
    {
        public IGroup Group => new GroupBuilder()
            .WithComponent<CameraFollowsComponent>()
            .WithComponent<CustomViewComponent>()
            .Build();

        public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var cameraFollows = entityComponentAccessor.GetComponent<CameraFollowsComponent>(entity);
            cameraFollows.Camera = Camera.main;
        }

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
        {
            var viewComponent = entityComponentAccessor.GetComponent<CustomViewComponent>(entity);
            var entityPosition = viewComponent.CustomView.transform.position;
            var trailPosition = entityPosition + (Vector3.back*5.0f);
            trailPosition += Vector3.up*2.0f;

            var camera = entityComponentAccessor.GetComponent<CameraFollowsComponent>(entity).Camera;
            camera.transform.position = trailPosition;
            camera.transform.LookAt(entityPosition);
        }
    }
}