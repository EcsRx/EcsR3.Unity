using SystemsR3.Events;
using EcsR3.Collections.Database;
using EcsR3.Entities;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.ManuallyRegisterSystems.Systems
{
    public class DefaultViewResolver : PrefabViewResolverSystem
    {
        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("Cube");

        public DefaultViewResolver(IEntityDatabase entityDatabase, IEventSystem eventSystem, IUnityInstantiator instantiator) 
            : base(entityDatabase, eventSystem, instantiator)
        {}

        protected override void OnViewCreated(IEntity entity, GameObject view)
        {
            view.name = $"entity-{entity.Id}";
        }
    }
}