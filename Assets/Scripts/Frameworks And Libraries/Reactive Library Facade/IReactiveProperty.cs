namespace ReactiveLibraryFacade
{
    public interface IReactiveProperty<T> : IReadonlyReactiveProperty<T>
    {
        public T Value { get; set; }
    }
}