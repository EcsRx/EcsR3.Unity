using EcsR3.Collections;
using EcsR3.Collections.Entity;
using EcsR3.Entities;
using UnityEngine;

namespace EcsR3.Unity.MonoBehaviours
{
    public class EntityView : MonoBehaviour
    {
        public IEntityCollection EntityCollection { get; set; }
        public IEntity Entity { get; set; }
    }
}