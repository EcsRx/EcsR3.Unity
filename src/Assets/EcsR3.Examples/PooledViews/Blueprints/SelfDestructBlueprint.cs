using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Examples.PooledViews.Components;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using UnityEngine;

namespace EcsR3.Examples.PooledViews.Blueprints
{
    public class SelfDestructBlueprint : IBlueprint
    {
        private readonly float _minLifetime = 1.0f;
        private readonly float _maxLifetime = 10.0f;
        private readonly Vector3 _startPosition;

        public SelfDestructBlueprint(Vector3 startPosition)
        {
            _startPosition = startPosition;
        }

        public void Apply(IEntity entity)
        {
            var selfDestructComponent = new SelfDestructComponent
            {
                Lifetime = Random.Range(_minLifetime, _maxLifetime),
                StartingPosition = _startPosition
            };

            var viewComponent = new ViewComponent();
            entity.AddComponents(selfDestructComponent, viewComponent);
        }
    }
}