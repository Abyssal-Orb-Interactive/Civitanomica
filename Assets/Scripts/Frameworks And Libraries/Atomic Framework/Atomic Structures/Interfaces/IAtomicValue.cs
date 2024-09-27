namespace AtomicFramework.AtomicStructures
{
    public interface IAtomicValue<out T>
    {
        public T CurrentValue { get; }
    }
}