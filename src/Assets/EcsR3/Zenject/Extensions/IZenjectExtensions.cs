using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using EcsR3.Collections;
using EcsR3.Groups;
using EcsR3.Groups.Observable;
using SystemsR3.Infrastructure.Dependencies;
using Zenject;

namespace EcsR3.Zenject.Extensions
{
    public static class ZenjectExtensions
    {
        public static DiContainer GetContainer(this IDependencyRegistry registry)
        { return registry.NativeRegistry as DiContainer; }
    
        public static DiContainer GetContainer(this IDependencyResolver resolver)
        { return resolver.NativeResolver as DiContainer; }
        
        public static IObservableGroup ResolveObservableGroup(this DiContainer container, IGroup group)
        {
            var observableGroupManager = container.Resolve<IObservableGroupManager>();
            return observableGroupManager.GetObservableGroup(group);
        }
        
        public static IObservableGroup ResolveObservableGroup(this DiContainer container, params Type[] componentTypes)
        {
            var observableGroupManager = container.Resolve<IObservableGroupManager>();
            var group = new Group(componentTypes);
            return observableGroupManager.GetObservableGroup(group);
        }
        
        public static IEnumerable ResolveAllOf(this DiContainer container, Type type)
        {
            return container.AllContracts
                .Where(bindingId => bindingId.Type == type)
                .Select(container.Resolve);
        }

        public static IEnumerable<T> ResolveAllOf<T>(this DiContainer container)
        { return container.ResolveAllOf(typeof(T)).Cast<T>(); }
    }
}