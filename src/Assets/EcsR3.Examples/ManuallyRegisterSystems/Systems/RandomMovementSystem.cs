using System;
using EcsR3.Computeds.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EcsR3.Examples.ManuallyRegisterSystems.Systems
{
    public class RandomMovementSystem : IReactToGroupSystem
    {
        public IGroup Group => new Group(typeof (ViewComponent));

        public Observable<IComputedEntityGroup> ReactToGroup(IComputedEntityGroup group)
        { return Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => group); }

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
            var positionChange = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            var gameObject = viewComponent.View as GameObject;
            gameObject.transform.position += positionChange;
        }
    }
}