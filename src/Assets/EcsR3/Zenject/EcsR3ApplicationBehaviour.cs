using System;
using EcsR3.Unity;
using EcsR3.Zenject.Dependencies;
using UnityEngine;
using Zenject;

namespace EcsR3.Zenject
{
    [DefaultExecutionOrder(-20000)]
    public abstract class EcsR3ApplicationBehaviour : UnityEcsR3ApplicationBehaviour
    {
        private Context _locatedContext;
        
        private void Awake()
        {
            var goContext = GetComponent<GameObjectContext>();
            if (goContext)
            {
                goContext.PostInstall += OnZenjectReady;
                _locatedContext = goContext;
                return;
            }
            
            var sceneContext = GetComponent<SceneContext>();
            if (sceneContext)
            {
                sceneContext.PostInstall += OnZenjectReady;
                _locatedContext = sceneContext;
                return;
            }

            var projectContext = ProjectContext.TryGetPrefab();
            if (projectContext)
            {
                _locatedContext = ProjectContext.Instance;
                ProjectContext.Instance.EnsureIsInitialized();
                OnZenjectReady();
                return;
            }
            
            throw new Exception("Cannot find Project, Scene or GameObject Context, please make sure one is on the application object");
        }

        /// <summary>
        /// Once the application has loaded get zenject container and whack it into our container
        /// </summary>
        protected void OnZenjectReady()
        {   
            DependencyRegistry = new ZenjectDependencyRegistry(_locatedContext.Container);
            StartApplication();
        }

        private void OnDestroy()
        {
            StopApplication();
        }

        /// <summary>
        /// Resolve any dependencies the application needs
        /// </summary>
        /// <remarks>By default it will setup SystemExecutor, EventSystem, EntityCollectionManager</remarks>
        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            _locatedContext.Container.Inject(this);
        }
    }
}