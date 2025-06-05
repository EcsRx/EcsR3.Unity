using System.Collections.Generic;
using System.Linq;
using EcsR3.Components;
using EcsR3.Unity.Extensions;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.UnityEditor.Data;
using EcsR3.UnityEditor.Editor.Helpers;
using EcsR3.UnityEditor.Editor.UIAspects;
using UnityEditor;

namespace EcsR3.UnityEditor.Editor
{
    [CustomEditor(typeof(EntityView))]
    public class EntityViewInspector : global::UnityEditor.Editor
    {
        private EntityView _entityView;
        private EntityDataUIAspect _entityDataAspect;
        private EntityData _entityDataProxy;
        
        private void OnEnable()
        {
            _entityView = (EntityView)target;
            _entityDataProxy = new EntityData();
            _entityDataAspect = new EntityDataUIAspect(_entityDataProxy, this);

            _entityDataProxy.EntityId = _entityView.Entity.Id;
            _entityDataProxy.Components = new List<IComponent>(_entityView.GetEcsComponents());
        }
        
        private void PoolSection()
        {
            EditorGUIHelper.WithVerticalLayout(() =>
            {
                EditorGUIHelper.WithVerticalLayout(() =>
                {
                    var entityId = _entityView.Entity.ToString();
                    EditorGUIHelper.WithLabelField("Entity Id", entityId);
                });
            });
        }
        
        private void SyncAnyExternalChanges()
        {
            var hasChanged = false;
            var components = _entityView.GetEcsComponents().ToArray();
            foreach (var component in components)
            {
                if (!_entityDataProxy.Components.Contains(component))
                {
                    _entityDataProxy.Components.Add(component);
                    hasChanged = true;
                }
            }
            
            for (var i = _entityDataProxy.Components.Count - 1; i >= 0; i--)
            {
                var previousComponent = _entityDataProxy.Components[i];
                if (!components.Contains(previousComponent))
                {
                    _entityDataProxy.Components.RemoveAt(i);
                    hasChanged = true;
                }
            }

            if(hasChanged)
            { Repaint(); }
        }

        public override void OnInspectorGUI()
        {
            _entityView = (EntityView)target;

            if (_entityView.Entity.Id == -1)
            {
                EditorGUILayout.LabelField("No Entity Assigned");
                return;
            }

            SyncAnyExternalChanges();

            PoolSection();

            _entityDataAspect.DisplayUI();
        }
    }
}