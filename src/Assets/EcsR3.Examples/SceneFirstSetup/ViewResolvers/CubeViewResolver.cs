using SystemsR3.Events;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using EcsR3.Examples.SceneFirstSetup.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.SceneFirstSetup.ViewResolvers
{
    public class CubeViewResolver : PrefabViewResolverSystem
    {
        private readonly Transform _parentTransform = GameObject.Find("Entities").transform;

        public override IGroup Group => base.Group.WithComponent<CubeComponent>();

        public CubeViewResolver(IEntityCollection entityCollection, IEventSystem eventSystem, IUnityInstantiator instantiator)
            : base(entityCollection, eventSystem, instantiator)
        {}

        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("Cube");
        protected override void OnViewCreated(IEntity entity, GameObject view)
        {
            view.transform.position = new Vector3(-2, 0, 0);
            view.transform.parent = _parentTransform;
        }
    }
}
