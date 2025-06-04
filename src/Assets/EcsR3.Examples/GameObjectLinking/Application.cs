using EcsR3.Unity.Extensions;
using EcsR3.Zenject;
using UnityEngine;

namespace EcsR3.Examples.GameObjectLinking
{
    public class Application : EcsR3ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var entity = EntityCollection.Create();

            var existingGameObject = GameObject.Find("ExistingGameObject");
            existingGameObject.LinkEntity(entity, EntityComponentAccessor);
        }
    }
}