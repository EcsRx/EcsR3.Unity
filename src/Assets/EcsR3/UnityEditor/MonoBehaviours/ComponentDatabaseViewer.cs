using EcsR3.Components.Database;
using EcsR3.Components.Lookups;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    /// <summary>
    /// If you are confused as to why you are seeing lots of components you are not using, remember that the
    /// component pool by default is optimistic in what components it pre-empts for, so you need to set
    /// the component config to not pre allocate for pools without config, they will still expand as
    /// normal, it just wont pre-emptively allocate ahead of time.
    /// </summary>
    public class ComponentDatabaseViewer : MonoBehaviour
    {
        [Inject]
        public IComponentDatabase ComponentDatabase { get; private set; }
        
        [Inject]
        public IComponentTypeLookup ComponentTypeLookup { get; private set; }
    }
}