using EcsR3.Collections;
using EcsR3.Components.Lookups;
using EcsR3.Computeds.Entities.Registries;
using SystemsR3.Executor;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ObservableGroupViewer : MonoBehaviour
    {
        [Inject]
        public IComputedEntityGroupRegistry ObservableGroupManager { get; private set; }
        
        [Inject]
        public IComponentTypeLookup ComponentTypeLookup { get; private set; }
        
        [Inject]
        public ISystemExecutor SystemExecutor { get; private set; }
    }
}