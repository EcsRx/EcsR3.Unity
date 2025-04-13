using System;
using System.Collections.Generic;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Infrastructure.Plugins;
using SystemsR3.Systems;
using EcsR3.Examples.PluginExample.HelloWorldPlugin.systems;

namespace EcsR3.Examples.PluginExample.HelloWorldPlugin
{
    public class HelloWorldPlugin : ISystemsR3Plugin
    {
        public string Name => "Hello World Plugin";
        public Version Version => new Version(1, 0, 0);

        public void SetupDependencies(IDependencyRegistry container)
        {
            container.Bind<OutputHelloWorldSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyResolver container)
        {
            return new[]
            {
                container.Resolve<OutputHelloWorldSystem>()
            };
        }
    }
}