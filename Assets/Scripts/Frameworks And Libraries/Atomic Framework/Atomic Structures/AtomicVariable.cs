using System;
using UnityEngine;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        [SerializeField] protected T _value = default;
        
        public T CurrentValue => _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        protected AtomicVariable(){}

        public AtomicVariable(T value)
        {
            _value = value;
        }
    }
}