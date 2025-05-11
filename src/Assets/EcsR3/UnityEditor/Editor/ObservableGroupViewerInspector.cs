using System.Collections.Generic;
using System.Linq;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Systems;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.Editor.UIAspects;
using EcsR3.UnityEditor.MonoBehaviours;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(ObservableGroupViewer))]
    public class ObservableGroupViewerInspector : global::UnityEditor.Editor
    {
        public class VisibilityState
        {
            public bool ShowGroup { get; set; }
            public bool ShowSystems { get; set; }
        }
        public Dictionary<LookupGroup, VisibilityState> VisibleStates = new();
        
        public override void OnInspectorGUI()
        {
            var observableGroupViewer = (ObservableGroupViewer)target;
            if(observableGroupViewer == null) {  return; }
            var observableGroupManager = observableGroupViewer.ObservableGroupManager;
            var componentTypeLookup = observableGroupViewer.ComponentTypeLookup;
            var systemExecutor = observableGroupViewer.SystemExecutor;

            if (observableGroupManager == null)
            {
                EditorGUILayout.LabelField("No Observable Groups");
                return;
            }
            
            EditorGUIHelper.WithLabel("Active Observable Groups");
            EditorGUILayout.Space();
            foreach (var observableGroup in observableGroupManager.ObservableGroups.OrderByDescending(x => x.Count))
            {
                if (!VisibleStates.ContainsKey(observableGroup.Group))
                { VisibleStates.Add(observableGroup.Group, new VisibilityState()); }
                var visibleState = VisibleStates[observableGroup.Group];
                
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = observableGroup.GetHashCode().ToMutedColor();
                    
                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        EditorGUILayout.LabelField("Entity Count:");
                        EditorGUILayout.LabelField(observableGroup.Count.ToString());
                    });

                    visibleState.ShowGroup = EditorGUIHelper.WithAccordion(visibleState.ShowGroup, "Group");

                    if (visibleState.ShowGroup)
                    { GroupUIAspect.DrawGroupUI(componentTypeLookup, observableGroup.Group); }

                    var systemsUsingGroup = systemExecutor.Systems
                        .Where(x => x is IGroupSystem groupSystem &&
                                    componentTypeLookup.GetLookupGroupFor(groupSystem.Group) == observableGroup.Group)
                        .ToArray();
                    
                    visibleState.ShowSystems = EditorGUIHelper.WithAccordion(visibleState.ShowSystems, $"Related Systems [{systemsUsingGroup.Length}]");
                    if (visibleState.ShowSystems)
                    {
                        EditorGUIHelper.WithVerticalBoxLayout(() =>
                        {
                            EditorGUI.indentLevel++;
                            foreach (var system in systemsUsingGroup)
                            { EditorGUILayout.LabelField(system.GetType().Name, SystemUIAspect.SystemTypeStyle); }
                            EditorGUI.indentLevel--;
                        });
                    }
                    
                    EditorGUILayout.Space();
                });
            }
        }
    }
}