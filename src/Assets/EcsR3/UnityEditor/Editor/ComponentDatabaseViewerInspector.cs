using System.Collections.Generic;
using System.Linq;
using EcsR3.Components.Database;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.MonoBehaviours;
using SystemsR3.Pools.Config;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(ComponentDatabaseViewer))]
    public class ComponentDatabaseViewerInspector : global::UnityEditor.Editor
    {
        private bool ShowUnallocatedPools { get; set; }
        
        public override void OnInspectorGUI()
        {
            var entityCollectionViewer = (ComponentDatabaseViewer)target;
            var componentDatabase = entityCollectionViewer.ComponentDatabase;
            var componentTypeLookup = entityCollectionViewer.ComponentTypeLookup;

            if (componentDatabase == null)
            {
                EditorGUILayout.LabelField("Component Database Inactive");
                return;
            }
            
            var concreteComponentDatabase = componentDatabase as ComponentDatabase;
            if (concreteComponentDatabase == null)
            {
                EditorGUILayout.LabelField("Component Database Viewer Currently Only Supports ComponentDatabase implementation");
                return;
            }
            
            EditorGUIHelper.WithLabel("Component Pools");
            var allComponentPoolStats = new List<(string componentName, int currentCount, int currentMax, PoolConfig poolConfig)>();
            foreach (var componentPool in concreteComponentDatabase.ComponentData)
            {
                allComponentPoolStats.Add(new(componentPool.ComponentType.Name,
                    componentPool.Count - componentPool.IndexesRemaining, componentPool.Count,
                    concreteComponentDatabase.GetPoolConfigFor(componentPool.ComponentType)));
            }
            
            EditorGUILayout.Space();
            foreach (var componentPoolStats in allComponentPoolStats.Where(x => x.currentMax > 0))
            {
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = componentPoolStats.componentName.GetHashCode().ToMutedColor();

                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        EditorGUILayout.LabelField($"{componentPoolStats.componentName}");
                        EditorGUILayout.LabelField($"{componentPoolStats.currentCount}/{componentPoolStats.currentMax} Allocated");
                    });
                });
            }

            EditorGUIHelper.WithVerticalBoxLayout(() =>
            {
                GUI.backgroundColor = string.Empty.GetHashCode().ToMutedColor();
                var nonPreAllocated = allComponentPoolStats
                    .Where(x => x.currentMax == 0)
                    .ToArray();
                
                ShowUnallocatedPools = EditorGUIHelper.WithAccordion(ShowUnallocatedPools, $"{nonPreAllocated.Length} Unallocated Pools");
                if (!ShowUnallocatedPools) { return; }

                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    EditorGUI.indentLevel++;
                    if (nonPreAllocated.Length > 0)
                    {
                        foreach (var componentPoolStats in nonPreAllocated)
                        { EditorGUILayout.LabelField($"{componentPoolStats.componentName}"); }
                    }
                    else
                    { EditorGUILayout.LabelField("No Unallocated Pools"); }
                    EditorGUI.indentLevel--;
                });
            });

        }
    }
}