using System;
using System.Collections.Generic;
using System.Reflection;

namespace AtomicFramework.AtomicObject
{
    public static class ReflectionUtils
    {
        private static readonly Dictionary<Type, FieldInfo[]> FieldsMap = new();
        private static readonly Dictionary<Type, PropertyInfo[]> PropertiesMap = new();
        private static readonly Dictionary<Type, MethodInfo[]> MethodsMap = new();

        internal static FieldInfo[] GetFields(Type targetType)
        {
            if (FieldsMap.TryGetValue(targetType, out var fields))
            {
                return fields;
            }

            fields = targetType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            FieldsMap.Add(targetType, fields);
            return fields;
        }

        internal static PropertyInfo[] GetProperties(Type targetType)
        {
            if (PropertiesMap.TryGetValue(targetType, out var properties))
            {
                return properties;
            }

            properties = targetType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            PropertiesMap.Add(targetType, properties);
            return properties;
        }

        internal static MethodInfo[] GetMethods(Type targetType)
        {
            if (MethodsMap.TryGetValue(targetType, out var methods))
            {
                return methods;
            }

            methods = targetType.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            MethodsMap.Add(targetType, methods);
            return methods;
        }
    }
}