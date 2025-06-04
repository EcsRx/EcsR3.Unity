using EcsR3.Components.Database;
using EcsR3.Computeds.Components.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.BatchedRandomReactions.Components;
using EcsR3.Systems.Batching.Convention;
using R3;
using SystemsR3.Attributes;
using SystemsR3.Threading;
using SystemsR3.Types;

namespace EcsR3.Examples.BatchedRandomReactions.Systems
{
    /// <summary>
    /// This is lower priority so happens after the other systems which have a higher priority
    /// </summary>
    [Priority(PriorityTypes.Lower)]
    public class BatchedCubeColourChangerSystem : BatchedSystem<ViewDataComponent, BatchedRandomColorComponent>
    {
        public BatchedCubeColourChangerSystem(IComponentDatabase componentDatabase, IEntityComponentAccessor entityComponentAccessor, IComputedComponentGroupRegistry computedComponentGroupRegistry, IThreadHandler threadHandler) : base(componentDatabase, entityComponentAccessor, computedComponentGroupRegistry, threadHandler)
        {}

        protected override Observable<Unit> ReactWhen()
        { return Observable.EveryUpdate().Select(x => Unit.Default); }

        protected override void Process(Entity entity, ViewDataComponent viewDataComponent, BatchedRandomColorComponent randomColorComponent)
        {
            // We just quit out if there is no change for this entity
            if (!randomColorComponent.HasChanged) { return; }

            // if so we just get the already cached view data and apply the color and reset the flag
            viewDataComponent.Material.color = randomColorComponent.Color;
            randomColorComponent.HasChanged = false;
        }
    }
}