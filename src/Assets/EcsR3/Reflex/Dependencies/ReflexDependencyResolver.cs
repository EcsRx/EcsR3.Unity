using System;
using System.Collections;
using System.Collections.Generic;
using EcsR3.Unity.Dependencies;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace EcsR3.Reflex.Dependencies
{
    public class ReflexDependencyResolver : IUnityInstantiator
    {
        private readonly Container _container;
        private readonly Dictionary<(Type FromType, string Name), Type> _namedBindings;
        public object NativeResolver => _container;

        public ReflexDependencyResolver(Container container, Dictionary<(Type FromType, string Name), Type> namedBindings)
        {
            _container = container;
            _namedBindings = namedBindings;
        }

        public object Resolve(Type type, string name = null)
        {
            if(string.IsNullOrEmpty(name))
            { return _container.Resolve(type); }
            
            // Look up the named binding and resolve the concrete type
            if (_namedBindings.TryGetValue((type, name), out var concreteType))
            { return _container.Resolve(concreteType); }
            
            throw new InvalidOperationException($"No named binding found for type '{type.Name}' with name '{name}'");
        }

        public IEnumerable ResolveAll(Type type)
        { return _container.All(type); }

        public GameObject InstantiatePrefab(GameObject prefab)
        {
            var gameObject = GameObject.Instantiate(prefab);
            GameObjectInjector.InjectRecursive(gameObject, _container);
            return gameObject;
        }

        public void Dispose()
        {}
    }
}