using System;
using System.Linq;

namespace EcsR3.UnityEditor.Editor.Extensions
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if(!type.IsGenericType) { return type.Name; }
            
            var displayName =  type.Name[..type.Name.IndexOf("`", StringComparison.Ordinal)];
            var genericTypes = type.GetGenericArguments();
            return $"{displayName}<{string.Join(",", genericTypes.Select(x => x.Name))}>";
        }
    }
}