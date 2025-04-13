using System;
using EcsR3.Entities;
using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using R3;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions.Systems
{
    public class CubeColourChangerSystem : IReactToEntitySystem
    {
        public IGroup Group => new Group(typeof(ViewComponent), typeof(RandomColorComponent));

        private static int count;
        public Observable<IEntity> ReactToEntity(IEntity entity)
        {
            var colorComponent = entity.GetComponent<RandomColorComponent>();
            return colorComponent.Color.DistinctUntilChanged().Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var colorComponent = entity.GetComponent<RandomColorComponent>();
            var cubeComponent = entity.GetComponent<ViewComponent>();
            var view = cubeComponent.View as GameObject;
            var renderer = view.GetComponent<Renderer>();
            renderer.material.color = colorComponent.Color.Value;
        }
    }
}