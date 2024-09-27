using System;
using ReactiveLibraryFacade;
using ReactiveLibraryFacade.R3Wrappers;
using UnityEngine;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicReactiveProperty<T> : IAtomicVariable<T>, IReactiveProperty<T>
    {
        [SerializeField] private ReactivePropertyWrapper<T> _value = null;

        T IReadonlyReactiveProperty<T>.CurrentValue => _value.CurrentValue;
        T IAtomicValue<T>.CurrentValue => _value.CurrentValue;

        T IReactiveProperty<T>.Value
        {
            get => _value.Value;
            set => _value.Value = value;
        }
        T IAtomicVariable<T>.Value
        {
            get => _value.Value;
            set => _value.Value = value;
        }

        private AtomicReactiveProperty() {}

        private AtomicReactiveProperty(T value)
        {
            _value = new ReactivePropertyWrapper<T>(value);
        }

        public IDisposable Subscribe(ReactiveLibraryFacade.IObserver<T> observer)
        {
           return _value.Subscribe(observer);
        }

        public void Dispose()
        {
            _value?.Dispose();
        }

        public T Value { get; set; }
    }
}