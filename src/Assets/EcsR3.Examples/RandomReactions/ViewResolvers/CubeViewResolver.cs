﻿using EcsR3.Collections.Entities;
using SystemsR3.Events;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Unity.Dependencies;
using EcsR3.Unity.Systems;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions.ViewResolvers
{
    public class CubeViewResolver : PrefabViewResolverSystem
    {
        private const float _spacing = 2.0f;
        private const int _perRow = 20;
        private Vector3 _nextPosition = Vector3.zero;
        private int _currentOnRow = 0;

        protected override GameObject PrefabTemplate { get; } = Resources.Load<GameObject>("colored-cube");
        private Transform Parent { get; } = GameObject.Find("Entities").transform;
        
        public CubeViewResolver(IEntityCollection entityCollection, IEventSystem eventSystem, IUnityInstantiator instantiator)
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
            IncrementRow();
        }
    }
}