using System.Collections.Generic;
using System.Linq;
using EcsR3.Unity.Dependencies;

namespace EcsR3.Unity.Extensions
{
    public static class IUnityInstantiatorExtensions
    {
        public static T Resolve<T>(this IUnityInstantiator instantiator, string name = null)
        { return (T)instantiator.Resolve(typeof(T)); }
        
        public static IEnumerable<T> ResolveAll<T>(this IUnityInstantiator instantiator)
        { return instantiator.ResolveAll(typeof(T)).Cast<T>(); }
    }
}