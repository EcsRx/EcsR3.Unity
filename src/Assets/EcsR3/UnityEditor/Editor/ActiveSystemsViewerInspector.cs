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
                var systemType = system.GetType();
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = system.GetHashCode().ToMutedColor();
                    EditorGUILayout.LabelField(systemType.Name);

                    EditorGUILayout.LabelField("Implements");
                    
                    EditorGUIHelper.WithVerticalBoxLayout(() =>
                    {
                        var interfacesImplemented = system.GetSystemTypesImplemented();
                        foreach (var interfaceImplemented in interfacesImplemented)
                        {
                            EditorGUILayout.LabelField(interfaceImplemented.Name, requiredComponentStyle);
                        }
                    });
                    
                    if (system is IGroupSystem groupSystem)
                    {
                        EditorGUILayout.LabelField("System Components");

                        EditorGUIHelper.WithVerticalBoxLayout(() =>
                        {
                            foreach (var componentType in groupSystem.Group.RequiredComponents)
                            {
                                EditorGUILayout.LabelField(componentType.Name, requiredComponentStyle);
                            }
                        });

                        if (groupSystem.Group.ExcludedComponents.Length > 0)
                        {
                            EditorGUIHelper.WithVerticalBoxLayout(() =>
                            {
                                foreach (var componentType in groupSystem.Group.ExcludedComponents)
                                {
                                    EditorGUILayout.LabelField(componentType.Name, excludedComponentStyle);
                                }
                            });
                        }
                    }
                });
            }
        }
    }
}