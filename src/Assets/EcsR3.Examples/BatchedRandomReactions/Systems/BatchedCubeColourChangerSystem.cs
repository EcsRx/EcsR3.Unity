using EcsR3.Collections;
using EcsR3.Components.Database;
using EcsR3.Components.Lookups;
using EcsR3.Examples.BatchedRandomReactions.Components;
using EcsR3.Plugins.Batching.Factories;
using EcsR3.Plugins.Batching.Systems;
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
    public class BatchedCubeColourChangerSystem : ReferenceBatchedSystem<ViewDataComponent, BatchedRandomColorComponent>
    {
        public BatchedCubeColourChangerSystem(IComponentDatabase componentDatabase, IComponentTypeLookup componentTypeLookup, IReferenceBatchBuilderFactory batchBuilderFactory, IThreadHandler threadHandler, IObservableGroupManager observableGroupManager) : base(componentDatabase, componentTypeLookup, batchBuilderFactory, threadHandler, observableGroupManager)
        {}
        
        protected override Observable<bool> ReactWhen()
        { return Observable.EveryUpdate().Select(x => true); }

        protected override void Process(int entityId, ViewDataComponent viewDataComponent, BatchedRandomColorComponent randomColorComponent)
        {
            // We just quit out if there is no change for this entity
            if (!randomColorComponent.HasChanged) { return; }

            // if so we just get the already cached view data and apply the color and reset the flag
            viewDataComponent.Material.color = randomColorComponent.Color;
            randomColorComponent.HasChanged = false;
        }
    }
}