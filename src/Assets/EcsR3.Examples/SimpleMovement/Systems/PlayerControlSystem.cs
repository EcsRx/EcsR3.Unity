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
    public class PlayerControlSystem : IBasicEntitySystem
    {
        public readonly float MovementSpeed = 2.0f;

        public IGroup Group => new GroupBuilder()
            .WithComponent<ViewComponent>()
            .WithComponent<PlayerControlledComponent>()
            .Build();

        public void Process(IEntity entity, ElapsedTime elapsedTime)
        {
            var strafeMovement = 0f;
            var forardMovement = 0f;

            if (Input.GetKey(KeyCode.A)) { strafeMovement = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { strafeMovement = 1.0f; }
            if (Input.GetKey(KeyCode.W)) { forardMovement = 1.0f; }
            if (Input.GetKey(KeyCode.S)) { forardMovement = -1.0f; }

            var viewComponent = entity.GetComponent<ViewComponent>();
            var view = viewComponent.View as GameObject;
            var transform = view.transform;

            var newPosition = view.transform.position;
            newPosition.x += strafeMovement * MovementSpeed * Time.deltaTime;
            newPosition.z += forardMovement * MovementSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}