using EcsR3.Components.Lookups;
using EcsR3.Groups;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.UIAspects
{
    public static class GroupUIAspect
    {
        public static GUIStyle RequiredComponentStyle = new()
        {
            normal = new GUIStyleState() { textColor = Color.green.Desaturate(0.5f)}
        };
        
        public static GUIStyle ExcludedComponentStyle = new()
        {
            normal = new GUIStyleState() { textColor = Color.red.Desaturate(0.5f)}
        };
        
        public static void DrawGroupUI(IComponentTypeLookup componentTypeLookup, LookupGroup group)
        {
            EditorGUIHelper.WithVerticalBoxLayout(() =>
            {
                EditorGUI.indentLevel++;
                foreach (var componentTypeId in group.RequiredComponents)
                {
                    var componentType = componentTypeLookup.GetComponentType(componentTypeId);
                    EditorGUILayout.LabelField(componentType.Name, RequiredComponentStyle);
                }
                EditorGUI.indentLevel--;
            });

            if (group.ExcludedComponents.Length > 0)
            {
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    EditorGUI.indentLevel++;
                    foreach (var componentTypeId in group.ExcludedComponents)
                    {
                        var componentType = componentTypeLookup.GetComponentType(componentTypeId);
                        EditorGUILayout.LabelField(componentType.Name, ExcludedComponentStyle);
                    }
                    EditorGUI.indentLevel--;
                });
            }
        }
        
        public static void DrawGroupUI(IGroup group)
        {
            EditorGUIHelper.WithVerticalBoxLayout(() =>
            {
                EditorGUI.indentLevel++;
                foreach (var componentType in group.RequiredComponents)
                { EditorGUILayout.LabelField(componentType.Name, RequiredComponentStyle); }
                EditorGUI.indentLevel--;
            });

            if (group.ExcludedComponents.Length > 0)
            {
                EditorGUIHelper.WithVerticalBoxLayout(() =>
                {
                    EditorGUI.indentLevel++;
                    foreach (var componentType in group.ExcludedComponents)
                    { EditorGUILayout.LabelField(componentType.Name, ExcludedComponentStyle); }
                    EditorGUI.indentLevel--;
                });
            }
        }
    }
}