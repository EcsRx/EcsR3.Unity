using EcsR3.Reflex.Dependencies;
using EcsR3.Unity;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.Pool;

namespace EcsR3.Reflex
{
    [DefaultExecutionOrder(-20000)]
    public abstract class EcsR3ApplicationBehaviour : UnityEcsR3ApplicationBehaviour
    {
        private Container _container;

        private void Awake()
        {
            DependencyRegistry = new ReflexDependencyRegistry();
            StartApplication();
        }

        private void OnDestroy()
        {
            StopApplication();
        }
        
        private void InjectScene()
        {
            using var pooledObject1 = ListPool<GameObject>.Get(out var rootGameObjects);
            gameObject.scene.GetRootGameObjects(rootGameObjects);
            GameObjectInjector.InjectRecursiveMany(rootGameObjects, _container);
            AttributeInjector.Inject(this, _container);
        }

        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            _container = DependencyResolver.NativeResolver as Container;
            InjectScene();
        }
    }
}