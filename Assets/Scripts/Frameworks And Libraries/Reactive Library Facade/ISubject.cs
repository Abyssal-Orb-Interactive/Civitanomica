namespace ReactiveLibraryFacade
{
    public interface ISubject : IObservable
    {
        public void InvokeChanging();
    }
    
    public interface ISubject<T> : IObservable<T>
    {
        public void InvokeChanging(T newValue);
    }
}