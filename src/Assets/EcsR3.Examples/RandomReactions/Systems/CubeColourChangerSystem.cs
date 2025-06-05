using System;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using EcsR3.Systems.Reactive;
using R3;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions.Systems
{
    public class CubeColourChangerSystem : IReactToEntitySystem
    {
        public IGroup Group => new Group(typeof(ViewComponent), typeof(RandomColorComponent));

        private static int count;
        
        public Observable<Entity> ReactToEntity(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var colorComponent = entityComponentAccessor.GetComponent<RandomColorComponent>(entity);
            return colorComponent.Color.DistinctUntilChanged().Select(x => entity);
        }

        public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var colorComponent = entityComponentAccessor.GetComponent<RandomColorComponent>(entity);
            var cubeComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
            var view = cubeComponent.View as GameObject;
            var renderer = view.GetComponent<Renderer>();
            renderer.material.color = colorComponent.Color.Value;
        }
    }
}