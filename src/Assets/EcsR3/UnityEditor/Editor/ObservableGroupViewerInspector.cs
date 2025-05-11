using System.Linq;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.MonoBehaviours;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(ObservableGroupViewer))]
    public class ObservableGroupViewerInspector : global::UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var observableGroupViewer = (ObservableGroupViewer)target;
            if(observableGroupViewer == null) {  return; }
            var observableGroupManager = observableGroupViewer.ObservableGroupManager;
            var componentTypeLookup = observableGroupViewer.ComponentTypeLookup;
            var visibleStates = observableGroupViewer.VisibleStates;

            if (observableGroupManager == null)
            {
                EditorGUILayout.LabelField("No Observable Groups");
                return;
            }
            
            var requiredComponentStyle = new GUIStyle() { };
            requiredComponentStyle.normal.textColor = Color.green.Desaturate(0.5f);
            var excludedComponentStyle = new GUIStyle() { };
            excludedComponentStyle.normal.textColor = Color.red.Desaturate(0.5f);
            
            EditorGUIHelper.WithLabel("Active Observable Groups");
            EditorGUILayout.Space();
            foreach (var observableGroup in observableGroupManager.ObservableGroups.OrderByDescending(x => x.Count))
            {
                if (!visibleStates.ContainsKey(observableGroup.Group))
                { visibleStates.Add(observableGroup.Group, new ObservableGroupViewer.VisibilityState()); }
                var visibleState = visibleStates[observableGroup.Group];
                
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    GUI.backgroundColor = observableGroup.GetHashCode().ToMutedColor();
                    
                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        EditorGUILayout.LabelField("Entity Count:");
                        EditorGUILayout.LabelField(observableGroup.Count.ToString());
                    });
                    
                    EditorGUIHelper.WithHorizontalBoxLayout(() =>
                    {
                        var iconStyle = new GUIStyle { fontSize = 12, stretchWidth = true };
                        iconStyle.normal.textColor = Color.white;
                        EditorGUI.indentLevel++;
                        visibleState.ShowGroup =
                            EditorGUILayout.Foldout(visibleState.ShowGroup, "Group", true);
                        EditorGUI.indentLevel--;
                    });

                    if (!visibleState.ShowGroup) { return; }
                    
                    EditorGUIHelper.WithVerticalBoxLayout(() =>
                    {
                        EditorGUI.indentLevel++;
                        foreach (var componentTypeId in observableGroup.Group.RequiredComponents)
                        {
                            var componentType = componentTypeLookup.GetComponentType(componentTypeId);
                            EditorGUILayout.LabelField(componentType.Name, requiredComponentStyle);
                        }
                        EditorGUI.indentLevel--;
                    });

                    if (observableGroup.Group.ExcludedComponents.Length > 0)
                    {
                        EditorGUIHelper.WithVerticalBoxLayout(() =>
                        {
                            EditorGUI.indentLevel++;
                            foreach (var componentTypeId in observableGroup.Group.ExcludedComponents)
                            {
                                var componentType = componentTypeLookup.GetComponentType(componentTypeId);
                                EditorGUILayout.LabelField(componentType.Name, excludedComponentStyle);
                            }
                            EditorGUI.indentLevel--;
                        });
                    }
                });
            }
        }
    }
}