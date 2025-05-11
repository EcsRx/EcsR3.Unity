using System.Collections.Generic;
using EcsR3.Collections;
using SystemsR3.Executor;
using SystemsR3.Systems;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ActiveSystemsViewer : MonoBehaviour
    {
        public class VisibilityState
        {
            public bool ShowImplementations { get; set; } 
            public bool ShowGroup { get; set; }
        }
        
        [Inject]
        public ISystemExecutor SystemExecutor { get; private set; }
        
        [Inject]
        public IObservableGroupManager ObservableGroupManager { get; private set; }
        
        public Dictionary<ISystem, VisibilityState> VisibleStates = new();
    }
}