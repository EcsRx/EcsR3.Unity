using System;
using System.Collections.Generic;
using EcsR3.Unity.Dependencies;
using Reflex.Core;
using Reflex.Enums;
using SystemsR3.Infrastructure.Dependencies;

namespace EcsR3.Reflex.Dependencies
{
    public class ReflexDependencyRegistry : IDependencyRegistry
    {
        private readonly Dictionary<(Type FromType, string Name), Type> _namedBindings = new();
        
        private ContainerBuilder _containerBuilder;
        public object NativeRegistry => _containerBuilder;

        private ReflexDependencyResolver _resolver;

        public ReflexDependencyRegistry()
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterValue(this, new[] { typeof(IDependencyRegistry) });
            _containerBuilder.RegisterFactory(x => _resolver, new[]{ typeof(IDependencyResolver), typeof(IUnityInstantiator) }, Lifetime.Singleton, Resolution.Lazy);
        }

        public void LoadModule(IDependencyModule module)
        { module.Setup(this); }

        public void Bind(Type fromType, Type toType, BindingConfiguration configuration = null)
        {
            var lifetime = configuration?.AsSingleton == false ? Lifetime.Transient : Lifetime.Singleton;

            if (configuration == null)
            {
                _containerBuilder.RegisterType(toType, new[] { fromType }, lifetime, Resolution.Lazy);
                return;
            }

            // TODO: This is a hack to work around lack of named bindings in Reflex, hopefully one day remove
            if (!string.IsNullOrEmpty(configuration.WithName))
            { _namedBindings[(fromType, configuration.WithName)] = toType; }

            if (configuration.ToInstance != null)
            { _containerBuilder.RegisterValue(configuration.ToInstance, new[] { fromType }); }
            else if (configuration.ToMethod != null)
            {
                _containerBuilder.RegisterFactory(x => configuration.ToMethod(_resolver), 
                    toType, new[] { fromType }, lifetime, Resolution.Lazy);
            }
            else
            { _containerBuilder.RegisterType(toType, new[] { fromType }, lifetime, Resolution.Lazy); }
        }

        public void Bind(Type type, BindingConfiguration configuration = null)
        { Bind(type, type, configuration); }

        public bool HasBinding(Type type, string name = null)
        { return _containerBuilder.HasBinding(type); }

        public void Unbind(Type type)
        {
            // Not supported
        }

        public IDependencyResolver BuildResolver()
        {
            var container = _containerBuilder.Build();
            _resolver = new ReflexDependencyResolver(container, _namedBindings);
            return _resolver;
        }

        public void Dispose()
        { _containerBuilder = null; }
    }
}