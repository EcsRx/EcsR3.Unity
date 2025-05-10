using SystemsR3.Events;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.ManuallyRegisterSystems.Systems
{
    public class DefaultViewResolver : PrefabViewResolverSystem
    {
        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("Cube");

        public DefaultViewResolver(IEntityCollection entityCollection, IEventSystem eventSystem, IUnityInstantiator instantiator) 
            : base(entityCollection, eventSystem, instantiator)
        {}

        protected override void OnViewCreated(IEntity entity, GameObject view)
        {
            view.name = $"entity-{entity.Id}";
        }
    }
}