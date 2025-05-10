using EcsR3.Plugins.Views.ViewHandlers;
using EcsR3.Unity.Dependencies;
using UnityEngine;

namespace EcsR3.Unity.Handlers
{
    public class PrefabViewHandler : IViewHandler
    {
        public IUnityInstantiator Instantiator { get; }
        protected GameObject PrefabTemplate { get; }
        
        public PrefabViewHandler(IUnityInstantiator instantiator, GameObject prefabTemplate)
        {
            Instantiator = instantiator;
            PrefabTemplate = prefabTemplate;
        }
        
        public virtual void DestroyView(object view)
        { Object.Destroy(view as GameObject); }

        public virtual void SetActiveState(object view, bool isActive)
        { (view as GameObject).SetActive(isActive); }

        public virtual object CreateView()
        {
            var createdPrefab = Instantiator.InstantiatePrefab(PrefabTemplate);
            createdPrefab.transform.position = Vector3.zero;
            createdPrefab.transform.rotation = Quaternion.identity;
            return createdPrefab;
        }
    }
}