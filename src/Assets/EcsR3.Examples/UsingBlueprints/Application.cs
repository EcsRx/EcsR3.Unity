using EcsR3.Examples.UsingBlueprints.Blueprints;
using EcsR3.Infrastructure.Extensions;
using EcsR3.Unity;
using EcsR3.Unity.Extensions;
using EcsR3.Zenject;
using EcsR3.Zenject.Extensions;

namespace EcsR3.Examples.UsingBlueprints
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityDatabase.GetCollection();

            defaultPool.CreateEntity(new PlayerBlueprint("Player One"));
            defaultPool.CreateEntity(new PlayerBlueprint("Player Two", 150.0f));
        }
    }
}