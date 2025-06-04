using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems.Reactive;
using UnityEngine;

namespace EcsR3.Examples.GameObjectLinking.Systems
{
    public class ChangeScaleOnLinkingSystem : ISetupSystem
    {
        public IGroup Group { get; } = new Group(typeof(ViewComponent));

        public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
        {
            var viewComponent = entityComponentAccessor.GetComponent<ViewComponent>(entity);
            var view = viewComponent.View as GameObject;
            view.transform.localScale = Vector3.one*3;
        }
    }
}