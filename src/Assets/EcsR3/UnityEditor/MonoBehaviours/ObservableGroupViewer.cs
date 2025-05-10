using EcsR3.Collections;
using EcsR3.Components.Lookups;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ObservableGroupViewer : MonoBehaviour
    {
        [Inject]
        public IObservableGroupManager ObservableGroupManager { get; private set; }
        
        [Inject]
        public IComponentTypeLookup ComponentTypeLookup { get; private set; }
    }
}