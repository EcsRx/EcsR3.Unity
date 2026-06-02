using System;
using System.Collections.Generic;
using System.Linq;
using Reflex.Core;
using Reflex.Enums;
using SystemsR3.Systems;

namespace EcsR3.Reflex.Helpers
{
    public class BindSystemsInNamespace
    {
        public ContainerBuilder ContainerBuilder { get; }
        public IEnumerable<string> SystemNamespaces { get; }

        public BindSystemsInNamespace(ContainerBuilder containerBuilder, IEnumerable<string> systemNamespaces)
        {
            ContainerBuilder = containerBuilder;
            SystemNamespaces = systemNamespaces;
        }

        public static void Bind(ContainerBuilder containerBuilder, IEnumerable<string> systemNamespaces)
        {
            new BindSystemsInNamespace(containerBuilder, systemNamespaces).Bind();
        }

        public void Bind()
        {
            var systemTypes = GetAllApplicableSystemsInNamespace();
            foreach (var systemType in systemTypes)
            {
                ContainerBuilder.RegisterType(systemType, new[] { typeof(ISystem) }, Lifetime.Singleton, Resolution.Lazy);
            }
        }

        private IEnumerable<Type> GetAllApplicableSystemsInNamespace()
        {
            var applicationAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return applicationAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(IsWithinNamespace)
                .Where(NoAbstractOrInterfaces)
                .Where(IsApplicableSystem);
        }

        private bool IsWithinNamespace(Type type)
        {
            return type.Namespace != null && SystemNamespaces.Any(ns => type.Namespace.Contains(ns));
        }

        private bool NoAbstractOrInterfaces(Type type)
        { return !type.IsInterface && !type.IsAbstract; }

        private bool IsApplicableSystem(Type possibleSystemType)
        {
            var isApplicable = possibleSystemType == typeof(ISystem) || typeof(ISystem).IsAssignableFrom(possibleSystemType);
            return isApplicable;
        }
    }
}