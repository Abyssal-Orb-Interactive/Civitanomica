using ReactiveLibraryFacade;

namespace AtomicFramework.AtomicStructures
{
    public interface IAtomicEvent : IObservable, IAtomicAction
    {
    }

    public interface IAtomicEvent<T> : IObservable<T>, IAtomicAction<T>
    {
    }

    public interface IAtomicEvent<T1, T2> : IObservable<T1, T2>, IAtomicAction<T1, T2>
    {
    }

    public interface IAtomicEvent<T1, T2, T3> : IObservable<T1, T2, T3>, IAtomicAction<T1, T2, T3>
    {
    }
}