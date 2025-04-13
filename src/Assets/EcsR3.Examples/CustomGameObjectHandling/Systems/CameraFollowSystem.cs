using EcsR3.Entities;
using EcsR3.Examples.CustomGameObjectHandling.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
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

        public void Setup(IEntity entity)
        {
            var cameraFollows = entity.GetComponent<CameraFollowsComponent>();
            cameraFollows.Camera = Camera.main;
        }

        public void Process(IEntity entity, ElapsedTime elapsedTime)
        {
            var entityPosition = entity.GetComponent<CustomViewComponent>().CustomView.transform.position;
            var trailPosition = entityPosition + (Vector3.back*5.0f);
            trailPosition += Vector3.up*2.0f;

            var camera = entity.GetComponent<CameraFollowsComponent>().Camera;
            camera.transform.position = trailPosition;
            camera.transform.LookAt(entityPosition);
        }
    }
}