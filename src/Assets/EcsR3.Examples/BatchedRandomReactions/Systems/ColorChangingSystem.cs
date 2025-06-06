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
using UnityEngine;
using Random = UnityEngine.Random;

namespace EcsR3.Examples.BatchedRandomReactions.Systems
{
    /// <summary>
    /// Although we dont use the ViewDataComponent it lets us use the same batch as the other system meaning its
    /// faster/less memory, if we had lots of systems using slight variations then maybe makes sense to have
    /// separate batches, but for here as its only 2 systems we will just share given how many entities there are
    /// </summary>
    [Priority(PriorityTypes.Higher)]
    public class BatchedColorChangingSystem : BatchedSystem<ViewDataComponent, BatchedRandomColorComponent>
    {
        private readonly float MaxDelay = 5.0f;
        private readonly float MinDelay = 1.0f;

        public BatchedColorChangingSystem(IComponentDatabase componentDatabase, IEntityComponentAccessor entityComponentAccessor, IComputedComponentGroupRegistry computedComponentGroupRegistry, IThreadHandler threadHandler) : base(componentDatabase, entityComponentAccessor, computedComponentGroupRegistry, threadHandler)
        {}

        protected override Observable<Unit> ReactWhen()
        { return Observable.EveryUpdate().Select(x => Unit.Default); }

        protected override void Process(Entity entity, ViewDataComponent _, BatchedRandomColorComponent randomColorComponent)
        {
            randomColorComponent.Elapsed += Time.deltaTime;

            if (!(randomColorComponent.Elapsed >= randomColorComponent.NextChangeIn))
            { return; }

            randomColorComponent.Elapsed -= randomColorComponent.NextChangeIn;
            randomColorComponent.NextChangeIn = Random.Range(MinDelay, MaxDelay);

            var r = Random.Range(0.0f, 1.0f);
            var g = Random.Range(0.0f, 1.0f);
            var b = Random.Range(0.0f, 1.0f);
            randomColorComponent.Color = new Color(r,g,b);
            randomColorComponent.HasChanged = true;
        }
    }
}