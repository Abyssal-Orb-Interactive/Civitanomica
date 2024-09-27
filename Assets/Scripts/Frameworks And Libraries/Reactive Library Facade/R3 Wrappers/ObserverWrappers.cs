using R3;

namespace ReactiveLibraryFacade.R3Wrappers
{
    public class ObserverWrapper<T> : IObserver<T>
    {
        public Observer<T> Observer { get; } = null;

        public ObserverWrapper(Observer<T> observer)
        {
            Observer = observer;
        }

        public void OnChange(T value)
        {
            Observer.OnNext(value);
        }

        public void Dispose()
        {
            Observer?.Dispose();
        }
    }
    
    public class ObserverWrapper : IObserver
    {
        public Observer<Unit> Observer { get; } = null;

        public ObserverWrapper(Observer<Unit> observer)
        {
            Observer = observer;
        }

        public void OnChange()
        {
            Observer.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            Observer?.Dispose();
        }
    }
}