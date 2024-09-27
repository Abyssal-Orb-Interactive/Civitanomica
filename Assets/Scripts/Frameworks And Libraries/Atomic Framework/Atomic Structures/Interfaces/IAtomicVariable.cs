namespace AtomicFramework.AtomicStructures
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        public T Value { get; set; }
    }
}