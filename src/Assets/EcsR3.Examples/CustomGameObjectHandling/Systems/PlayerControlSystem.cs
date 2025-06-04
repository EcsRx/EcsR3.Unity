using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.CustomGameObjectHandling.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using SystemsR3.Scheduling;
using UnityEngine;

namespace EcsR3.Examples.CustomGameObjectHandling.Systems
{
    public class PlayerControlSystem : IBasicEntitySystem
    {
        public readonly float MovementSpeed = 2.0f;

        public IGroup Group => new GroupBuilder()
            .WithComponent<CustomViewComponent>()
            .WithComponent<PlayerControlledComponent>()
            .Build();

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
        {
            var strafeMovement = 0f;
            var forardMovement = 0f;

            if (Input.GetKey(KeyCode.A)) { strafeMovement = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { strafeMovement = 1.0f; }
            if (Input.GetKey(KeyCode.W)) { forardMovement = 1.0f; }
            if (Input.GetKey(KeyCode.S)) { forardMovement = -1.0f; }

            var viewComponent = entityComponentAccessor.GetComponent<CustomViewComponent>(entity);
            var transform = viewComponent.CustomView.transform;
            var newPosition = transform.position;

            newPosition.x += strafeMovement * MovementSpeed * Time.deltaTime;
            newPosition.z += forardMovement * MovementSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}