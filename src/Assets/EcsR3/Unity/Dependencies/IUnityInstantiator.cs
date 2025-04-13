using SystemsR3.Infrastructure.Dependencies;
using UnityEngine;

namespace EcsR3.Unity.Dependencies
{
    public interface IUnityInstantiator : IDependencyResolver
    {
        GameObject InstantiatePrefab(GameObject prefab);
    }
}