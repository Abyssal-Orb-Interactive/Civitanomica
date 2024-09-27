using System;
using JetBrains.Annotations;

namespace AtomicFramework.AtomicObject.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class GetAttribute : Attribute
    {
        internal readonly string ID = null;
        internal readonly bool Override = false;

        public GetAttribute(string id, bool @override = false)
        {
            ID = id;
            Override = @override;
        }
    }

}