using EcsR3.Collections.Database;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class EntityDatabaseViewer : MonoBehaviour
    {
         [Inject]
         public IEntityDatabase EntityDatabase { get; private set; }
    }
}