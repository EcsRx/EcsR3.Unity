﻿using EcsR3.Components.Database;
using EcsR3.Examples.BatchedRandomReactions.Components;
using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;
using SystemsR3.Pools.Config;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions
{
    public class Application : EcsR3ApplicationBehaviour
    {
        private readonly int _cubeCount = 5000;

        public override ComponentDatabaseConfig OverrideComponentDatabaseConfig()
        {
            return new ComponentDatabaseConfig()
            {
                PoolSpecificConfig =
                {
                    { typeof(ViewComponent), new PoolConfig(_cubeCount) },
                    { typeof(ViewDataComponent), new PoolConfig(_cubeCount) },
                    { typeof(BatchedRandomColorComponent), new PoolConfig(_cubeCount) },
                }
            };
        }

        protected override void ApplicationStarted()
        {
            for (var i = 0; i < _cubeCount; i++)
            {
                var viewEntity = EntityCollection.Create();
                EntityComponentAccessor.AddComponents(viewEntity, new ViewComponent(), new RandomColorComponent());
            }

            var group = ComputedEntityGroupRegistry.GetComputedGroup(new Group(typeof(ViewComponent), typeof(RandomColorComponent)));
            Debug.Log($"There are {group.Count} entities out of {EntityCollection.Count} matching");
        }
    }
}