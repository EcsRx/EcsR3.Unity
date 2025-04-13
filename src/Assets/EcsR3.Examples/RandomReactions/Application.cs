using EcsR3.Examples.RandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Infrastructure.Extensions;
using EcsR3.Unity;
using EcsR3.Unity.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;
using EcsR3.Zenject.Extensions;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions
{
    public class Application : EcsR3ApplicationBehaviour
    {
        private readonly int _cubeCount = 5000;
        
        protected override void ApplicationStarted()
        {
            var collection = EntityDatabase.GetCollection();

            for (var i = 0; i < _cubeCount; i++)
            {
                var viewEntity = collection.CreateEntity();
                viewEntity.AddComponents(new ViewComponent(), new RandomColorComponent());
            }

            var group = ObservableGroupManager.GetObservableGroup(new Group(typeof(ViewComponent), typeof(RandomColorComponent)));
            Debug.Log($"There are {group.Count} entities out of {collection.Count} matching");
        }
    }
}