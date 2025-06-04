using EcsR3.Examples.GameObjectBinding.Components;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;

namespace EcsR3.Examples.GameObjectBinding
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var cubeEntity = EntityCollection.Create();
            EntityComponentAccessor.CreateComponent<ViewComponent>(cubeEntity);
            EntityComponentAccessor.CreateComponent<CubeComponent>(cubeEntity);

            var sphereEntity = EntityCollection.Create();
            EntityComponentAccessor.CreateComponent<ViewComponent>(cubeEntity);
            EntityComponentAccessor.CreateComponent<SphereComponent>(cubeEntity);
        }
    }
}