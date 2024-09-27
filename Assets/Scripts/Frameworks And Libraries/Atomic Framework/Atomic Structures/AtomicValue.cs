using System;
using UnityEngine;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public sealed class AtomicValue<T> : IAtomicValue<T>
    {
        [SerializeField] private T _value = default;

        public T CurrentValue => _value;
        
        private AtomicValue(){}
        public AtomicValue(T value)
        {
            _value = value;
        }
    }
}