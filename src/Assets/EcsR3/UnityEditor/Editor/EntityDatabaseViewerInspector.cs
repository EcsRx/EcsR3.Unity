using System.Linq;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.MonoBehaviours;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(EntityDatabaseViewer))]
    public class EntityDatabaseViewerInspector : global::UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var entityDatabaseViewer = (EntityDatabaseViewer)target;
            var entityDatabase = entityDatabaseViewer.EntityDatabase;

            if (entityDatabase == null)
            {
                EditorGUILayout.LabelField("Entity Database Inactive");
                return;
            }
            
            EditorGUIHelper.WithLabel("Collections");

            var totalEntities = 0;
            foreach (var entityCollection in entityDatabase.Collections)
            {
                totalEntities += entityCollection.Count;
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = entityCollection.GetHashCode().ToMutedColor();
                    EditorGUIHelper.WithHorizontalLayout(() =>
                    {
                        EditorGUILayout.LabelField($"Collection {entityCollection.Id}");
                        EditorGUILayout.LabelField($"Entities {entityCollection.Count}");
                    });
                });
            }

            EditorGUILayout.Space();
            
            EditorGUIHelper.WithVerticalBoxLayout(() =>
            {
                EditorGUILayout.LabelField("Total Entities", totalEntities.ToString());
            });
        }
    }
}