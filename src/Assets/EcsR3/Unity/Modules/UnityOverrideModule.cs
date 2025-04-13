using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Scheduling;
using EcsR3.Unity.Scheduling;

namespace EcsR3.Unity.Modules
{
    public class UnityOverrideModule : IDependencyModule 
    {
        public void Setup(IDependencyRegistry  registry)
        {
            registry.Unbind<IUpdateScheduler>();
            registry.Bind<IUpdateScheduler, UnityUpdateScheduler>(x => x.AsSingleton());
        }
    }
}