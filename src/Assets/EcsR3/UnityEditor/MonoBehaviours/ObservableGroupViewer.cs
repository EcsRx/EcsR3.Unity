using System.Collections.Generic;
using EcsR3.Collections;
using EcsR3.Components.Lookups;
using EcsR3.Groups;
using EcsR3.Groups.Observable;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ObservableGroupViewer : MonoBehaviour
    {
        public class VisibilityState
        {
            public bool ShowGroup { get; set; }
        }
        
        [Inject]
        public IObservableGroupManager ObservableGroupManager { get; private set; }
        
        [Inject]
        public IComponentTypeLookup ComponentTypeLookup { get; private set; }
        
        public Dictionary<LookupGroup, VisibilityState> VisibleStates = new();
    }
}