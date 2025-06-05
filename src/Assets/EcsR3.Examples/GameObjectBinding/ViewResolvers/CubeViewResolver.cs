using EcsR3.Collections.Entities;
using SystemsR3.Events;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.GameObjectBinding.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.GameObjectBinding.ViewResolvers
{
    public class CubeViewResolver : PrefabViewResolverSystem
    {
        public override IGroup Group => base.Group.WithComponent<CubeComponent>();

        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("Cube");

        public CubeViewResolver(IEntityCollection entityCollection, IEventSystem eventSystem, IUnityInstantiator instantiator) 
            : base(entityCollection, eventSystem, instantiator)
        {}

        protected override void OnViewCreated(IEntityComponentAccessor entityComponentAccessor, Entity entity, GameObject view)
        {
            view.transform.position = new Vector3(-2, 0, 0);
        }
    }
}