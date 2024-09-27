namespace AtomicFramework.AtomicStructures
{
    public interface IAtomicFunction<out R> : IAtomicValue<R>
    {
        public R Invoke();
        
        R IAtomicValue<R>.CurrentValue => Invoke();
    }

    public interface IAtomicFunction<in T, out R>
    {
        public R Invoke(T args);
    }
    
    public interface IAtomicFunction<in T1, in T2, out R>
    {
        public R Invoke(T1 args1, T2 args2);
    }
}