using EcsR3.Entities;
using EcsR3.Examples.SimpleMovement.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using SystemsR3.Scheduling;
using UnityEngine;

namespace EcsR3.Examples.SimpleMovement.Systems
{
    public class CameraFollowSystem : ISetupSystem, IBasicEntitySystem
    {
        public IGroup Group => new GroupBuilder()
            .WithComponent<CameraFollowsComponent>()
            .WithComponent<ViewComponent>()
            .Build();

        public void Setup(IEntity entity)
        {
            var cameraFollows = entity.GetComponent<CameraFollowsComponent>();
            cameraFollows.Camera = Camera.main;
        }

        public void Process(IEntity entity, ElapsedTime elapsedTime)
        {
            var viewComponent = entity.GetComponent<ViewComponent>();
            var view = viewComponent.View as GameObject;
            var entityPosition = view.transform.position;
            var trailPosition = entityPosition + (Vector3.back*5.0f);
            trailPosition += Vector3.up*2.0f;

            var camera = entity.GetComponent<CameraFollowsComponent>().Camera;
            camera.transform.position = trailPosition;
            camera.transform.LookAt(entityPosition);
        }
    }
}