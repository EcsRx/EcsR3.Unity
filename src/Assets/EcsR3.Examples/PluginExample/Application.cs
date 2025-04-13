using EcsR3.Examples.PluginExample.HelloWorldPlugin.components;
using EcsR3.Extensions;
using EcsR3.Unity;
using EcsR3.Unity.Extensions;
using EcsR3.Zenject;

namespace EcsR3.Examples.PluginExample
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void LoadPlugins()
        {
            base.LoadPlugins();
            RegisterPlugin(new HelloWorldPlugin.HelloWorldPlugin());
        }
        
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityDatabase.GetCollection();
            var entity = defaultPool.CreateEntity();
            entity.AddComponent<SayHelloWorldComponent>();
        }
    }
}