using System;
using EcsR3.UnityEditor.Editor.Extensions;
using EcsR3.UnityEditor.Editor.UIAspects;
using EcsR3.UnityEditor.MonoBehaviours;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace EcsR3.UnityEditor.Editor
{
    [Serializable]
    [CustomEditor(typeof(RegisterAsEntity))]
    public partial class RegisterAsEntityInspector : UEditor
    {
        private RegisterAsEntity _registerAsEntity;
        private EntityDataUIAspect _entityDataAspect;

        private void CollectionSection()
        {
            this.UseVerticalBoxLayout(() =>
            {
                _registerAsEntity.CollectionId = this.WithNumberField("Override Collection Id:", _registerAsEntity.CollectionId);
                _registerAsEntity.EntityId = this.WithNumberField("Override Entity Id:", _registerAsEntity.EntityId);
            });
        }

        private void OnEnable()
        {
            _registerAsEntity = (RegisterAsEntity)target;
            _entityDataAspect = new EntityDataUIAspect(_registerAsEntity.EntityData, this);
            _registerAsEntity.DeserializeState();

            if (_registerAsEntity.EntityData.EntityId != 0)
            { _registerAsEntity.EntityId = _registerAsEntity.EntityData.EntityId; }
        }

        private void PersistChanges()
        {
            _registerAsEntity.EntityId = _registerAsEntity.EntityData.EntityId;
            if (GUI.changed)
            {
                _registerAsEntity.SerializeState();
                this.SaveActiveSceneChanges();
            }
        }

        public override void OnInspectorGUI()
        {
            CollectionSection();
            _entityDataAspect.DisplayUI();
            PersistChanges();
        }
    }
}
