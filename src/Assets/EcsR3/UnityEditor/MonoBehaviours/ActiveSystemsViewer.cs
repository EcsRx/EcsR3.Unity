using EcsR3.Collections;
using EcsR3.Computeds.Entities.Registries;
using SystemsR3.Executor;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ActiveSystemsViewer : MonoBehaviour
    {
        [Inject]
        public ISystemExecutor SystemExecutor { get; private set; }
        
        [Inject]
        public IComputedEntityGroupRegistry ObservableGroupManager { get; private set; }
    }
}