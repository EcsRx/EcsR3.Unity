using System;
using System.Collections;
using System.Collections.Generic;
using EcsR3.Computeds.Entities;
using EcsR3.Computeds.Entities.Registries;
using EcsR3.Groups;
using Reflex.Core;
using SystemsR3.Infrastructure.Dependencies;

namespace EcsR3.Reflex.Extensions
{
    public static class ReflexExtensions
    {
        public static Container GetContainer(this IDependencyRegistry registry)
        { return registry.NativeRegistry as Container; }

        public static Container GetContainer(this IDependencyResolver resolver)
        { return resolver.NativeResolver as Container; }

        public static IComputedEntityGroup ResolveObservableGroup(this Container container, IGroup group)
        {
            var observableGroupManager = container.Resolve<IComputedEntityGroupRegistry>();
            return observableGroupManager.GetComputedGroup(group);
        }

        public static IComputedEntityGroup ResolveObservableGroup(this Container container, params Type[] componentTypes)
        {
            var observableGroupManager = container.Resolve<IComputedEntityGroupRegistry>();
            var group = new Group(componentTypes);
            return observableGroupManager.GetComputedGroup(group);
        }

        public static IEnumerable ResolveAllOf(this Container container, Type type)
        {
            return container.All(type);
        }

        public static IEnumerable<T> ResolveAllOf<T>(this Container container)
        { return container.All<T>(); }
    }
}