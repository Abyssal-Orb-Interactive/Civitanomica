using System;

namespace AtomicFramework.AtomicObject
{
    internal sealed class ReferenceInfo
    {
        internal readonly string ID = null;
        internal readonly bool Override = false;
        internal readonly Func<object, object> Value = null;

        internal ReferenceInfo(string id, bool @override, Func<object, object> value)
        {
            ID = id;
            Override = @override;
            Value = value;
        }
    }
}