using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Examples.BatchedRandomReactions.Components;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Plugins.Views.Components;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using SystemsR3.Events;
using UnityEngine;

namespace EcsR3.Examples.BatchedRandomReactions.ViewResolvers
{
    public class SphereViewResolver : PrefabViewResolverSystem
    {
        private const float _spacing = 2.0f;
        private const int _perRow = 20;
        private Vector3 _nextPosition = Vector3.zero;
        private int _currentOnRow = 0;

        public override IGroup Group { get; } = new Group(typeof(ViewComponent), typeof(ViewDataComponent));

        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("coloured-sphere");
        private Transform Parent { get; } = GameObject.Find("Entities").transform;

        public SphereViewResolver(IEntityCollection entityCollection, IEventSystem eventSystem,
            IUnityInstantiator instantiator)
            : base(entityCollection, eventSystem, instantiator)
        {}
        
        private void IncrementRow()
        {
            _currentOnRow++;

            if (_currentOnRow < _perRow)
            {
                _nextPosition.x += _spacing;
                return;
            }

            _currentOnRow = 0;
            _nextPosition.x = 0.0f;
            _nextPosition.z += _spacing;
        }

        protected override void OnViewCreated(IEntityComponentAccessor entityComponentAccessor, Entity entity, GameObject view)
        {
            view.transform.position = _nextPosition;
            view.transform.SetParent(Parent);

            // Get the renderer material and cache it to constantly call GetComponent
            var viewDataComponent = entityComponentAccessor.GetComponent<ViewDataComponent>(entity);
            viewDataComponent.Material = view.GetComponent<Renderer>().material;
            
            IncrementRow();
        }
    }
}