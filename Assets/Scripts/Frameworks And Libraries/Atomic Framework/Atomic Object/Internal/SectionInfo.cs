using System.Collections.Generic;
using System.Reflection;

namespace AtomicFramework.AtomicObject
{
    internal sealed class SectionInfo
    {
        internal readonly IEnumerable<string> Types = null;
        internal readonly IEnumerable<ReferenceInfo> References = null;
        internal readonly IEnumerable<SectionInfo> Children = null;

        private readonly FieldInfo _field = null;

        internal SectionInfo(
            IEnumerable<string> types,
            IEnumerable<ReferenceInfo> references,
            IEnumerable<SectionInfo> children,
            FieldInfo field
        )
        {
            Types = types;
            References = references;
            Children = children;
            _field = field;
        }

        internal object GetValue(object parent)
        {
            return _field.GetValue(parent);
        }
    }
}