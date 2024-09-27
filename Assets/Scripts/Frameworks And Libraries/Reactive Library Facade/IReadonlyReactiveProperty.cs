namespace ReactiveLibraryFacade
{
    public interface IReadonlyReactiveProperty<out T> : IObservable<T>
    {
        public T CurrentValue { get; }
    }
}