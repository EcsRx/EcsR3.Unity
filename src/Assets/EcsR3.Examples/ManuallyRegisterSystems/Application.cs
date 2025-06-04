using SystemsR3.Infrastructure.Extensions;
using EcsR3.Examples.ManuallyRegisterSystems.Systems;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Zenject;
using SystemsR3.Systems;
using UnityEngine;

namespace EcsR3.Examples.ManuallyRegisterSystems
{
    public class Application : EcsR3ApplicationBehaviour
    {
        // We override this to stop auto bindings
        protected override void BindSystems()
        {
            DependencyRegistry.Bind<RandomMovementSystem>();
        }
        
        // We override this to manually control how systems start
        protected override void StartSystems()
        {
            // This one we are manually binding and starting at the same time
            this.BindAndStartSystem<DefaultViewResolver>();
            
            // This one we are manually starting from the installer which has already bound it
            this.StartSystem<RandomMovementSystem>();
        }

        protected override void ApplicationStarted()
        {
            var entity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(entity, new ViewComponent());
        }
    }
}