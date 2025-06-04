using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using Zenject;

namespace EcsR3.Unity.MonoBehaviours
{
    public class EntityView : InjectableMonoBehaviour
    {
        [Inject]
        public IEntityComponentAccessor EntityComponentAccessor { get; protected set; }

        public Entity Entity { get; set; } = new(-1, 0);
        
        public override void DependenciesResolved()
        {  }
    }
}