using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Systems;
using UnityEngine;

namespace EcsR3.Examples.GameObjectLinking.Systems
{
    public class ChangeScaleOnLinkingSystem : ISetupSystem
    {
        public IGroup Group { get; } = new GroupWithPredicate(x => {
            var viewComponent = x.GetComponent<ViewComponent>();
            return viewComponent.View != null;
        }, typeof(ViewComponent));

        public void Setup(IEntity entity)
        {
            var viewComponent = entity.GetComponent<ViewComponent>();
            var view = viewComponent.View as GameObject;
            view.transform.localScale = Vector3.one*3;
        }
    }
}