using System;
using JetBrains.Annotations;

namespace AtomicFramework.AtomicObject.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class IsAttribute : Attribute
    {
        internal readonly string[] Types = null;

        public IsAttribute(params string[] types)
        {
            Types = types;
        }
    }
}