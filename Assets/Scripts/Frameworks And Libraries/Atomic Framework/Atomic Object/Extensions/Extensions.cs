using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicObject.Extensions
{
    public static class Extensions
    {
        public static void AddComponent(this AtomicObjectBase it, object component, bool @override = true)
        {
            var componentType = component.GetType();

            var types = AtomicScanner.ScanTypes(componentType);
            it.AddTypes(types);

            var references = AtomicScanner.ScanReferences(componentType);

            if (@override)
            {
                foreach (var reference in references)
                {
                    var key = reference.ID;
                    var value = reference.Value(component);

                    if (reference.Override)
                    {
                        it.SetData(key, value);
                    }
                    else
                    {
                        it.AddData(key, value);
                    }
                }
            }
            else
            {
                foreach (var reference in references)
                {
                    var key = reference.ID;
                    var value = reference.Value(component);
                    it.AddData(key, value);
                }
            }
        }

        public static void RemoveComponent(this AtomicObjectBase it, object component)
        {
            it.RemoveComponent(component.GetType());
        }

        public static void RemoveComponent<T>(this AtomicObjectBase it)
        {
            it.RemoveComponent(typeof(T));
        }

        public static void RemoveComponent(this AtomicObjectBase it, Type componentType)
        {
            var types = AtomicScanner.ScanTypes(componentType);
            it.RemoveTypes(types);

            var references = AtomicScanner.ScanReferences(componentType);
            foreach (var reference in references)
            {
                it.RemoveData(reference.ID);
            }
        }

        public static void CopyDataFrom(this AtomicObjectBase it, IAtomicObject other, bool @override = true)
        {
            if (@override)
            {
                foreach (var (key, value) in other.GetAll())
                {
                    it.SetData(key, value);
                }
            }
            else
            {
                foreach (var (key, value) in other.GetAll())
                {
                    it.AddData(key, value);
                }
            }
        }

        public static void CopyTypesFrom(this AtomicObjectBase it, IAtomicObject other)
        {
            it.AddTypes(other.Types());
        }
    }
}