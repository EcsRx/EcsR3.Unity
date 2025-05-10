using System;
using System.Linq;
using EcsR3.Unity;
using EcsR3.Zenject.Dependencies;
using UnityEngine;
using Zenject;

namespace EcsR3.Zenject
{
    [DefaultExecutionOrder(-20000)]
    public abstract class EcsR3ApplicationBehaviour : UnityEcsR3ApplicationBehaviour
    {
        private SceneContext _sceneContext;
        
        private void Awake()
        {
            var sceneContexts = FindObjectsByType<SceneContext>(FindObjectsSortMode.None);
            _sceneContext = sceneContexts.FirstOrDefault();
            
            if(_sceneContext == null) 
            { throw new Exception("Cannot find SceneContext, please make sure one is on the scene"); }
            
            _sceneContext.PostInstall += OnZenjectReady;
        }

        /// <summary>
        /// Once the application has loaded get zenject container and whack it into our container
        /// </summary>
        protected void OnZenjectReady()
        {   
            DependencyRegistry = new ZenjectDependencyRegistry(_sceneContext.Container);
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
            _sceneContext.Container.Inject(this);
        }
    }
}