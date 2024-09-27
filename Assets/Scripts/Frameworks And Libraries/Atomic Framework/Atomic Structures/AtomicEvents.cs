using System;
using ReactiveLibraryFacade;
using ReactiveLibraryFacade.R3Wrappers;
using Sirenix.OdinInspector;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public class AtomicEvent : IAtomicEvent
    {
        private ISubject _subject = null;

        public AtomicEvent()
        {
            _subject = new SubjectWrapper();
        }

        public IDisposable Subscribe(IObserver observer)
        {
            return _subject.Subscribe(observer);
        }

        [Button]
        public virtual void Invoke()
        {
            _subject.InvokeChanging();
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }

    [Serializable]
    public class AtomicEvent<T> : IAtomicEvent<T>
    {
        private ISubject<T> _subject = null;

        public AtomicEvent()
        {
            _subject = new SubjectWrapper<T>();
        }

        public IDisposable Subscribe(ReactiveLibraryFacade.IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }

        [Button]
        public virtual void Invoke(T value)
        {
            _subject.InvokeChanging(value);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }
}