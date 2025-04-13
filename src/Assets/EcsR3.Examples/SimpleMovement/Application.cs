using EcsR3.Examples.SimpleMovement.Components;
using EcsR3.Extensions;
using EcsR3.Infrastructure.Extensions;
using EcsR3.Unity;
using EcsR3.Unity.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;
using EcsR3.Zenject.Extensions;
using UnityEngine;

namespace EcsR3.Examples.SimpleMovement
{
    public class Application : EcsR3ApplicationBehaviour
    {        
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityDatabase.GetCollection();
            var viewEntity = defaultPool.CreateEntity();
            viewEntity.AddComponents(new ViewComponent(), 
                new PlayerControlledComponent(), new CameraFollowsComponent());
        }
    }
}