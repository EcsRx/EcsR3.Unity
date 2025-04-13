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
            var entityCollection = EntityDatabase.GetCollection();

            var cubeEntity = entityCollection.CreateEntity();
            cubeEntity.AddComponent<ViewComponent>();
            cubeEntity.AddComponent<CubeComponent>();

            var sphereEntity = entityCollection.CreateEntity();
            sphereEntity.AddComponent<ViewComponent>();
            sphereEntity.AddComponent<SphereComponent>();
        }
    }
}