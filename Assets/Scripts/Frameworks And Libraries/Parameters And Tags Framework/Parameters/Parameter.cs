using MessagePack;
using UnityEngine;
using System;

namespace ParametersAndTagsFramework
{
    [Serializable]
    [MessagePackObject(true)]
    public abstract class Parameter
    {
        
        [field: SerializeField]
        [IgnoreMember] public string Name { get; }

        protected Parameter(string name)
        {
            Name = name;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Parameter other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return $"{Name} {nameof(Parameter)}";
        }
    }
}