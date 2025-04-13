using System;
using System.Collections;
using EcsR3.Unity.Dependencies;
using EcsR3.Zenject.Extensions;
using UnityEngine;
using Zenject;

namespace EcsR3.Zenject.Dependencies
{
    public class ZenjectDependencyResolver : IUnityInstantiator
    {
        private readonly DiContainer _container;
        public object NativeResolver => _container;

        public ZenjectDependencyResolver(DiContainer container)
        {
            _container = container;
        }

        public object Resolve(Type type, string name = null)
        {
            return string.IsNullOrEmpty(name) ? 
                _container.TryResolve(type) : 
                _container.TryResolveId(type, name);
        }
        
        public IEnumerable ResolveAll(Type type)
        { return _container.ResolveAllOf(type); }

        public GameObject InstantiatePrefab(GameObject prefab)
        { return _container.InstantiatePrefab(prefab); }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}