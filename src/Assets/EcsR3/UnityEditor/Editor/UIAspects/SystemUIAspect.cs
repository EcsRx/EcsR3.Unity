using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.Helpers;
using SystemsR3.Extensions;
using SystemsR3.Systems;
using UnityEditor;
using UnityEngine;

namespace EcsR3.UnityEditor.Editor.UIAspects
{
    public static class SystemUIAspect
    {
        public static GUIStyle SystemTypeStyle = new()
        {
            normal = new GUIStyleState() { textColor = Color.green.Desaturate(0.5f) }
        };

        public static void DrawSystemTypesUI(ISystem system)
        {
            EditorGUIHelper.WithVerticalBoxLayout(() => {
                EditorGUI.indentLevel++;
                
                var interfacesImplemented = system.GetSystemTypesImplemented();
                foreach (var interfaceImplemented in interfacesImplemented)
                { EditorGUILayout.LabelField(interfaceImplemented.GetFriendlyName(), SystemTypeStyle); }
                
                EditorGUI.indentLevel--;
            });
        }
    }
}