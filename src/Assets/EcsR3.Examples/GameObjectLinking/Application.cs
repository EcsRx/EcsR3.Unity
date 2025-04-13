using EcsR3.Infrastructure.Extensions;
using EcsR3.Unity;
using EcsR3.Unity.Extensions;
using EcsR3.Zenject;
using EcsR3.Zenject.Extensions;
using UnityEngine;

namespace EcsR3.Examples.GameObjectLinking
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityDatabase.GetCollection();
            var entity = defaultPool.CreateEntity();

            var existingGameObject = GameObject.Find("ExistingGameObject");
            existingGameObject.LinkEntity(entity, defaultPool);
        }
    }
}