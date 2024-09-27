using System;
using JetBrains.Annotations;

namespace AtomicFramework.AtomicObject.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SectionAttribute : Attribute
    {
    }
}