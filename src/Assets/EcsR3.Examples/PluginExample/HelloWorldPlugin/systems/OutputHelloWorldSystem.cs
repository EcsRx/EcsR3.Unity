using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.PluginExample.HelloWorldPlugin.components;
using EcsR3.Groups;
using EcsR3.Systems;
using EcsR3.Systems.Reactive;
using UnityEngine;

namespace EcsR3.Examples.PluginExample.HelloWorldPlugin.systems
{
    public class OutputHelloWorldSystem : ISetupSystem
    {
        public IGroup Group => new Group(typeof(SayHelloWorldComponent));

        public void Setup(IEntityComponentAccessor entityComponentAccessor,  Entity entity)
        {
            Debug.Log($"Entity {entity.Id} Says Hello World");
        }
    }
}