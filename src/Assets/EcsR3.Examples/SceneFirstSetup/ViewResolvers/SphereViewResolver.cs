using SystemsR3.Events;
using EcsR3.Collections.Database;
using EcsR3.Entities;
using EcsR3.Examples.SceneFirstSetup.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.SceneFirstSetup.ViewResolvers
{
    public class SphereViewResolver : PrefabViewResolverSystem
    {
        private readonly Transform ParentTrasform = GameObject.Find("Entities").transform;

        public override IGroup Group => base.Group.WithComponent<SphereComponent>();

        public SphereViewResolver(IEntityDatabase entityDatabase, IEventSystem eventSystem, IUnityInstantiator instantiator)
            : base(entityDatabase, eventSystem, instantiator)
        {}

        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("Sphere");

        protected override void OnViewCreated(IEntity entity, GameObject view)
        {
            view.transform.position = new Vector3(2, 0, 0);
            view.transform.parent = ParentTrasform;
        }
    }
}
