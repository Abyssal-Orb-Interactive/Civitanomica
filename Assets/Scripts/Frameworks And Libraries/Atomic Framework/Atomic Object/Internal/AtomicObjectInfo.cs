using System.Collections.Generic;

namespace AtomicFramework.AtomicObject
{
    internal sealed class AtomicObjectInfo
    {
        internal readonly IEnumerable<string> Types = null;
        internal readonly IEnumerable<ReferenceInfo> References = null;
        internal readonly IEnumerable<SectionInfo> Sections = null;

        internal AtomicObjectInfo(
            IEnumerable<string> types,
            IEnumerable<ReferenceInfo> references,
            IEnumerable<SectionInfo> sections
        )
        {
            Types = types;
            References = references;
            Sections = sections;
        }
    }
}