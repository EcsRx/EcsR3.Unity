using System.Reflection;
using R3;
using UnityEngine;

namespace EcsR3.UnityEditor.Extensions
{ 
	public static class ComponentExtensions
	{
		public static JSONClass SerializeComponent(this object component)
		{
			var node = new JSONClass();
			foreach (var property in component.GetType().GetProperties())
			{
				if (property.CanRead && property.CanWrite)
				{
					if (property.PropertyType == typeof(int))
					{
						node.Add(property.Name, new JSONData((int)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<int>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<int>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<int> ();						
						node.Add(property.Name, new JSONData((int)reactiveProperty.Value));
						continue;
					}
					if (property.PropertyType == typeof(float))
					{
						node.Add(property.Name, new JSONData((float)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<float>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<float>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<float> ();						
						node.Add(property.Name, new JSONData((float)reactiveProperty.Value));
						continue;
					}
					if (property.PropertyType == typeof(bool))
					{
						node.Add(property.Name, new JSONData((bool)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<bool>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<bool>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<bool> ();						
						node.Add(property.Name, new JSONData((bool)reactiveProperty.Value));
						continue;
					}
					if (property.PropertyType == typeof(string))
					{
						node.Add(property.Name, new JSONData((string)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<string>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<string>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<string> ();						
						node.Add(property.Name, new JSONData((string)reactiveProperty.Value));
						continue;
					}
					if (property.PropertyType == typeof(Vector2))
					{
						node.Add(property.Name, new JSONData((Vector2)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<Vector2>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<Vector2>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<Vector2> ();					
						node.Add(property.Name, new JSONData((Vector2)reactiveProperty.Value));
						continue;
					}
					if (property.PropertyType == typeof(Vector3))
					{
						node.Add(property.Name, new JSONData((Vector3)property.GetValue(component, null)));
						continue;
					}
					if (property.PropertyType == typeof(ReactiveProperty<Vector3>))
					{
						var reactiveProperty = property.GetValue(component, null) as ReactiveProperty<Vector3>;
						if (reactiveProperty == null)
							reactiveProperty = new ReactiveProperty<Vector3> ();						
						node.Add(property.Name, new JSONData((Vector3)reactiveProperty.Value));
						continue;
					}
				}
			}
			return node;
		}

		public static void DeserializeComponent(this object component, JSONNode node)
		{
			foreach (var property in component.GetType().GetProperties())
			{
				ApplyValue(component, node, property);
			}
		}

		private static void ApplyValue(object component, JSONNode node, PropertyInfo property)
		{
			if (property.CanRead && property.CanWrite)
			{
				var propertyData = node[property.Name];
				if (propertyData == null) return;

				if (property.PropertyType == typeof(int))
				{
					property.SetValue(component, propertyData.AsInt, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<int>))
				{
					var reactiveProperty = new ReactiveProperty<int>(propertyData.AsInt);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
				if (property.PropertyType == typeof(float))
				{
					property.SetValue (component, propertyData.AsFloat, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<float>))
				{
					var reactiveProperty = new ReactiveProperty<float>(propertyData.AsFloat);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
				if (property.PropertyType == typeof(bool))
				{
					property.SetValue(component, propertyData.AsBool, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<bool>))
				{
					var reactiveProperty = new ReactiveProperty<bool>(propertyData.AsBool);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
				if (property.PropertyType == typeof(string))
				{
					property.SetValue(component, propertyData.Value, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<string>))
				{
					var reactiveProperty = new ReactiveProperty<string>(propertyData.Value);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
				if (property.PropertyType == typeof(Vector2))
				{
					property.SetValue(component, propertyData.AsVector2, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<Vector2>))
				{
					var reactiveProperty = new ReactiveProperty<Vector2>(propertyData.AsVector2);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
				if (property.PropertyType == typeof(Vector3))
				{
					property.SetValue(component, propertyData.AsVector3, null);
					return;
				}
				if (property.PropertyType == typeof(ReactiveProperty<Vector3>))
				{
					var reactiveProperty = new ReactiveProperty<Vector3>(propertyData.AsVector3);
					property.SetValue(component, reactiveProperty, null);
					return;
				}
			}
		}
	}
}