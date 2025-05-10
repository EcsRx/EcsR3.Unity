using System;
using System.Collections.Generic;
using EcsR3.Collections.Entity;
using EcsR3.Components;
using EcsR3.Entities;
using EcsR3.Extensions;
using EcsR3.Plugins.Views.Components;
using EcsR3.Unity.MonoBehaviours;
using EcsR3.UnityEditor.Data;
using EcsR3.UnityEditor.Extensions;
using UnityEngine;
using Zenject;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class RegisterAsEntity : MonoBehaviour
    {
        [Inject]
        public IEntityCollection EntityCollection { get; private set; }
        
        [SerializeField]
        public int EntityId;

        [SerializeField]
        public List<string> Components = new List<string>();

        [SerializeField]
        public List<string> ComponentEditorState = new List<string>();
        
        public bool HasDeserialized = false;
        public EntityData EntityData = new EntityData();
        
        public void SerializeState()
        {
            EntityData.EntityId = EntityId;

            try
            {
                Components.Clear();
                ComponentEditorState.Clear();
                foreach (var component in EntityData.Components)
                {
                    var componentName = component.ToString();
                    Components.Add(componentName);
                    var json = component.SerializeComponent();
                    ComponentEditorState.Add(json.ToString());
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Unable to Serialize [{gameObject.name}] - {e.Message}");
            }
        }

        public void DeserializeState()
        {
            EntityData.Components.Clear();
            
            for (var i = 0; i < Components.Count; i++)
            {
                var typeName = Components[i];
                var type = Type.GetType(typeName);
                if (type == null) { throw new Exception("Cannot resolve type for [" + typeName + "]"); }

                var component = (IComponent)Activator.CreateInstance(type);
                var componentProperties = JSON.Parse(ComponentEditorState[i]);
                component.DeserializeComponent(componentProperties);
                EntityData.Components.Add(component);
            }
            
            EntityData.EntityId = EntityId;
            HasDeserialized = true;
        }
        
        public IEntity CreateEntity()
        {
            if(EntityId > 0)
            { return EntityCollection.CreateEntity(null, EntityId); }

            return EntityCollection.CreateEntity();
        }
        
        [Inject]
        public void RegisterEntity()
        {
            if (!gameObject.activeInHierarchy || !gameObject.activeSelf) { return; }

            var createdEntity = CreateEntity();
            
            DeserializeState();
            createdEntity.AddComponents(EntityData.Components.ToArray());
            
            createdEntity.AddComponents(new ViewComponent { View = gameObject });
            SetupEntityBinding(createdEntity);

            Destroy(this);
        }

        private void SetupEntityBinding(IEntity entity)
        {
            var entityBinding = gameObject.AddComponent<EntityView>();
            entityBinding.Entity = entity;
            entityBinding.EntityCollection = EntityCollection;
        }
    }
}