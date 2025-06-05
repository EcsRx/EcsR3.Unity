using EcsR3.Examples.BatchedRandomReactions.Components;
using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;
using UnityEngine;

namespace EcsR3.Examples.BatchedRandomReactions
{
    public class Application : EcsR3ApplicationBehaviour
    {
        private readonly int _cubeCount = 5000;
        
        protected override void ApplicationStarted()
        {
            for (var i = 0; i < _cubeCount; i++)
            {
                var viewEntity = EntityCollection.Create();
                EntityComponentAccessor.AddComponents(viewEntity, new ViewComponent(), 
                    new BatchedRandomColorComponent(){ NextChangeIn = Random.Range(0.1f, 2.0f)}, 
                    new ViewDataComponent());
            }

            var group = ComputedEntityGroupRegistry.GetComputedGroup(new Group(typeof(ViewComponent), typeof(BatchedRandomColorComponent), typeof(ViewDataComponent)));
            Debug.Log($"There are {group.Count} entities out of {EntityCollection.Count} matching");
        }
    }
}