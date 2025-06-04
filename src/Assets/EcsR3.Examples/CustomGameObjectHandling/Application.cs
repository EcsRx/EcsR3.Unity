using EcsR3.Examples.CustomGameObjectHandling.Components;
using EcsR3.Extensions;
using EcsR3.Zenject;

namespace EcsR3.Examples.CustomGameObjectHandling
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var viewEntity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(viewEntity, new CustomViewComponent(), new PlayerControlledComponent(), new CameraFollowsComponent());
        }
    }
}