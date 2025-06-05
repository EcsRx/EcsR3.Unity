using EcsR3.Examples.SimpleMovement.Components;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;

namespace EcsR3.Examples.SimpleMovement
{
    public class Application : EcsR3ApplicationBehaviour
    {        
        protected override void ApplicationStarted()
        {
            var viewEntity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(viewEntity, new ViewComponent(), 
                new PlayerControlledComponent(), new CameraFollowsComponent());
        }
    }
}