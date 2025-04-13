using EcsR3.Examples.SceneFirstSetup.Components;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;

namespace EcsR3.Examples.SceneFirstSetup
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityDatabase.GetCollection();

            var cubeEntity = defaultPool.CreateEntity();
            cubeEntity.AddComponent<ViewComponent>();
            cubeEntity.AddComponent<CubeComponent>();

            var sphereEntity = defaultPool.CreateEntity();
            sphereEntity.AddComponent<ViewComponent>();
            sphereEntity.AddComponent<SphereComponent>();
        }
    }
}
