using System;
using System.Collections.Generic;

namespace AtomicFramework.AtomicObject
{
    public static class AtomicCompiler
    {
        private static readonly Dictionary<Type, AtomicObjectInfo> compiledObjects = new();

        ///Call it in background thread!
        public static void PrecompileObject(Type objectType)
        {
            CompileObject(objectType);
        }

        internal static AtomicObjectInfo CompileObject(Type objectType)
        {
            if (compiledObjects.TryGetValue(objectType, out var objectInfo))
            {
                return objectInfo;
            }

            objectInfo = CompileObjectInternal(objectType);
            compiledObjects.Add(objectType, objectInfo);
            return objectInfo;
        }

        private static AtomicObjectInfo CompileObjectInternal(Type objectType)
        {
            var types = new HashSet<string>();
            var references = new List<ReferenceInfo>();
            var sections = new List<SectionInfo>();

            foreach (var @interface in objectType.GetInterfaces())
            {
                types.UnionWith(AtomicScanner.ScanTypes(@interface));
                references.AddRange(AtomicScanner.ScanReferences(@interface));
            }

            while (objectType != typeof(AtomicObject))
            {
                types.UnionWith(AtomicScanner.ScanTypes(objectType));
                references.AddRange(AtomicScanner.ScanReferences(objectType));
                sections.AddRange(AtomicScanner.ScanSections(objectType));
                
                objectType = objectType!.BaseType;
            }

            return new AtomicObjectInfo(types, references, sections);
        }
    }
}