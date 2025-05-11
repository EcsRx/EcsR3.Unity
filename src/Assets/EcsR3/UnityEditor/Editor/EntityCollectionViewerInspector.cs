using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.MonoBehaviours;
using UnityEditor;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(EntityCollectionViewer))]
    public class EntityCollectionViewerInspector : global::UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var entityCollectionViewer = (EntityCollectionViewer)target;
            var entityCollection = entityCollectionViewer.EntityCollection;

            if (entityCollection == null)
            {
                EditorGUILayout.LabelField("Entity Database Inactive");
                return;
            }
            
            EditorGUIHelper.WithLabel("Collections");

            var totalEntities = entityCollection.Count;
            EditorGUILayout.Space();
            
            EditorGUIHelper.WithVerticalBoxLayout(() =>
            {
                EditorGUILayout.LabelField("Total Entities", totalEntities.ToString());
            });
        }
    }
}