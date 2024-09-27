using System;
using R3;
using UnityEngine;

namespace ReactiveLibraryFacade.R3Wrappers
{
    [Serializable]
    public class ReactivePropertyWrapper<T> : IReactiveProperty<T>
    {
        [SerializeField] private SerializableReactiveProperty<T> _reactiveProperty = null;

        public T CurrentValue => _reactiveProperty.CurrentValue;
        public T Value
        {
            get => _reactiveProperty.Value;
            set => _reactiveProperty.Value = value;
        }

        public ReactivePropertyWrapper(T value)
        {
            _reactiveProperty = new SerializableReactiveProperty<T>(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return observer is not ObserverWrapper<T> observerWrapper ? null : _reactiveProperty.Subscribe(observerWrapper.Observer);
        }

        public void Dispose()
        {
            _reactiveProperty?.Dispose();
        }
    }
    
    public class SubjectWrapper : ISubject
    {
        private readonly Subject<Unit> _subject = null;

        public SubjectWrapper()
        {
            _subject = new Subject<Unit>();
        }

        public IDisposable Subscribe(IObserver observer)
        {
            return observer is not ObserverWrapper observerWrapper ? null : _subject.Subscribe(observerWrapper.Observer);
        }

        public void InvokeChanging()
        {
            _subject.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _subject?.Dispose();
        }
    }
    
    public class SubjectWrapper<T> : ISubject<T>
    {
        private readonly Subject<T> _subject = null;

        public SubjectWrapper()
        {
            _subject = new Subject<T>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return observer is not ObserverWrapper<T> observerWrapper ? null : _subject.Subscribe(observerWrapper.Observer);
        }

        public void InvokeChanging(T newValue)
        {
            _subject.OnNext(newValue);
        }

        public void Dispose()
        {
            _subject?.Dispose();
        }
    }
}