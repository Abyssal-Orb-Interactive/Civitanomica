using System;
using System.Collections.Generic;
using System.Reflection;
using AtomicFramework.AtomicObject.Attributes;

namespace AtomicFramework.AtomicObject
{
    internal static class AtomicScanner
    {
        private static readonly Dictionary<Type, IEnumerable<string>> ScannedTypes = new();
        private static readonly Dictionary<Type, IEnumerable<ReferenceInfo>> ScannedReferences = new();
        private static readonly Dictionary<Type, IList<SectionInfo>> ScannedSections = new();

        internal static IEnumerable<string> ScanTypes(Type target)
        {
            if (ScannedTypes.TryGetValue(target, out var types))
            {
                return types;
            }

            types = ScanTypesInternal(target);
            ScannedTypes.Add(target, types);
            return types;
        }

        internal static IEnumerable<ReferenceInfo> ScanReferences(Type target)
        {
            if (ScannedReferences.TryGetValue(target, out var references))
            {
                return references;
            }

            references = ScanReferencesInternal(target);
            ScannedReferences.Add(target, references);
            return references;
        }

        internal static IEnumerable<SectionInfo> ScanSections(Type target)
        {
            if (ScannedSections.TryGetValue(target, out var sections))
            {
                return sections;
            }

            sections = new List<SectionInfo>();
            ScanSectionsInternal(target, sections);
            ScannedSections.Add(target, sections);
            return sections;
        }

        private static IEnumerable<string> ScanTypesInternal(Type target)
        {
            var attribute = target.GetCustomAttribute<IsAttribute>();

            return attribute != null ? attribute.Types : Array.Empty<string>();
        }

        private static IEnumerable<ReferenceInfo> ScanReferencesInternal(Type target)
        {
            var result = new List<ReferenceInfo>();

            var fields = ReflectionUtils.GetFields(target);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                var attribute = field.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.ID, attribute.Override, field.GetValue);
                result.Add(reference);
            }

            var properties = ReflectionUtils.GetProperties(target);
            for (int i = 0, count = properties.Length; i < count; i++)
            {
                var property = properties[i];
                var attribute = property.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.ID, attribute.Override, property.GetValue);
                result.Add(reference);
            }

            var methods = ReflectionUtils.GetMethods(target);
            for (int i = 0, count = methods.Length; i < count; i++)
            {
                var method = methods[i];
                var attribute = method.GetCustomAttribute<GetAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var reference = new ReferenceInfo(attribute.ID, attribute.Override, obj =>
                    method.Invoke(obj, Array.Empty<object>()));

                result.Add(reference);
            }

            return result;
        }


        private static void ScanSectionsInternal(Type parent, IList<SectionInfo> parentList)
        {
            var fields = ReflectionUtils.GetFields(parent);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];

                if (field.IsDefined(typeof(SectionAttribute)))
                {
                    parentList.Add(ScanSection(field));
                }
            }
        }

        private static SectionInfo ScanSection(FieldInfo sectionField)
        {
            var sectionType = sectionField.FieldType;
            var types = ScanTypes(sectionType);
            var references = ScanReferences(sectionType);
            var children = ScanSections(sectionType);
            return new SectionInfo(types, references, children, sectionField);
        }
    }
}