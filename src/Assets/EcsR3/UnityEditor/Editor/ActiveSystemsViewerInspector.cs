using EcsR3.Systems;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.MonoBehaviours;
using SystemsR3.Extensions;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(ActiveSystemsViewer))]
    public class ActiveSystemsViewerInspector : global::UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var activeSystemsViewer = (ActiveSystemsViewer)target;
            if(activeSystemsViewer == null) {  return; }
            var executor = activeSystemsViewer.SystemExecutor;
            var observableGroupManager = activeSystemsViewer.ObservableGroupManager;
            var visibleStates = activeSystemsViewer.VisibleStates;

            if (executor == null)
            {
                EditorGUILayout.LabelField("System Executor Inactive");
                return;
            }
            
            var requiredComponentStyle = new GUIStyle() { };
            requiredComponentStyle.normal.textColor = Color.green.Desaturate(0.5f);
            var excludedComponentStyle = new GUIStyle() { };
            excludedComponentStyle.normal.textColor = Color.red.Desaturate(0.5f);
            
            EditorGUIHelper.WithLabel("Running Systems");
            EditorGUILayout.Space();
            foreach (var system in executor.Systems)
            {
                if (!visibleStates.ContainsKey(system))
                { visibleStates.Add(system, new ActiveSystemsViewer.VisibilityState()); }
                
                var systemVisibleState = visibleStates[system];
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
                            var observableGroup = observableGroupManager.GetObservableGroup(groupedSystem.Group);
                            EditorGUILayout.LabelField($"{observableGroup.Count} Entities");
                        }
                    });

                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        var iconStyle = new GUIStyle { fontSize = 12, stretchWidth = true };
                        iconStyle.normal.textColor = Color.white;
                        EditorGUI.indentLevel++;
                        systemVisibleState.ShowImplementations =
                            EditorGUILayout.Foldout(systemVisibleState.ShowImplementations, "System Types", true);
                        EditorGUI.indentLevel--;
                    });

                    if (systemVisibleState.ShowImplementations)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUIHelper.WithVerticalBoxLayout(() =>
                        {
                            var interfacesImplemented = system.GetSystemTypesImplemented();
                            foreach (var interfaceImplemented in interfacesImplemented)
                            { EditorGUILayout.LabelField(interfaceImplemented.GetFriendlyName(), requiredComponentStyle); }
                        });
                        EditorGUI.indentLevel--;
                    }

                    if (system is not IGroupSystem groupSystem) { return; }
                    
                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        var iconStyle = new GUIStyle { fontSize = 12, stretchWidth = true };
                        iconStyle.normal.textColor = Color.white;
                        EditorGUI.indentLevel++;
                        systemVisibleState.ShowGroup =
                            EditorGUILayout.Foldout(systemVisibleState.ShowGroup, "Group", true);
                        EditorGUI.indentLevel--;
                    });

                    if (systemVisibleState.ShowGroup)
                    {
                        EditorGUIHelper.WithVerticalBoxLayout(() =>
                        {
                            EditorGUI.indentLevel++;
                            foreach (var componentType in groupSystem.Group.RequiredComponents)
                            {
                                EditorGUILayout.LabelField(componentType.Name, requiredComponentStyle);
                            }
                            EditorGUI.indentLevel--;
                        });

                        if (groupSystem.Group.ExcludedComponents.Length > 0)
                        {
                            EditorGUIHelper.WithVerticalBoxLayout(() =>
                            {
                                EditorGUI.indentLevel++;
                                foreach (var componentType in groupSystem.Group.ExcludedComponents)
                                {
                                    EditorGUILayout.LabelField(componentType.Name, excludedComponentStyle);
                                }
                                EditorGUI.indentLevel--;
                            });
                        }
                    }
                    
                });
            }
        }
    }
}