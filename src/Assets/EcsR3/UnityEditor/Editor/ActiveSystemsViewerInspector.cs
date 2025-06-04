using System.Collections.Generic;
using EcsR3.Systems;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.Editor.UIAspects;
using EcsR3.UnityEditor.MonoBehaviours;
using SystemsR3.Systems;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(ActiveSystemsViewer))]
    public class ActiveSystemsViewerInspector : global::UnityEditor.Editor
    {
        public class VisibilityState
        {
            public bool ShowImplementations { get; set; } 
            public bool ShowGroup { get; set; }
        }
        
        public Dictionary<ISystem, VisibilityState> VisibleStates = new();
        
        public override void OnInspectorGUI()
        {
            var activeSystemsViewer = (ActiveSystemsViewer)target;
            if(activeSystemsViewer == null) {  return; }
            var executor = activeSystemsViewer.SystemExecutor;
            var observableGroupManager = activeSystemsViewer.ObservableGroupManager;

            if (executor == null)
            {
                EditorGUILayout.LabelField("System Executor Inactive");
                return;
            }
            
            var requiredComponentStyle = new GUIStyle() { };
            requiredComponentStyle.normal.textColor = Color.green.Desaturate(0.5f);
            
            EditorGUIHelper.WithLabel("Running Systems");
            EditorGUILayout.Space();
            foreach (var system in executor.Systems)
            {
                if (!VisibleStates.ContainsKey(system))
                { VisibleStates.Add(system, new VisibilityState()); }
                
                var systemVisibleState = VisibleStates[system];
                var systemType = system.GetType();
                var groupedSystem = system as IGroupSystem;
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = system.GetHashCode().ToMutedColor();

                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        EditorGUILayout.LabelField(systemType.Name);
                        if (groupedSystem != null)
                        {
                            var observableGroup = observableGroupManager.GetComputedGroup(groupedSystem.Group);
                            EditorGUILayout.LabelField($"{observableGroup.Count} Entities");
                        }
                    });

                    systemVisibleState.ShowImplementations = EditorGUIHelper.WithAccordion(systemVisibleState.ShowImplementations, "System Types");
                    
                    if (systemVisibleState.ShowImplementations)
                    { SystemUIAspect.DrawSystemTypesUI(system); }

                    if (system is not IGroupSystem groupSystem) { return; }
                    
                    systemVisibleState.ShowGroup = EditorGUIHelper.WithAccordion(systemVisibleState.ShowGroup, "Group");

                    if (!systemVisibleState.ShowGroup) { return; }
                    GroupUIAspect.DrawGroupUI(groupSystem.Group);
                });
                EditorGUILayout.Space();
            }
        }
    }
}