namespace AtomicFramework.AtomicStructures
{
    public interface IAtomicAction
    {
       public void Invoke();
    }

    public interface IAtomicAction<in T>
    {
       public void Invoke(T args);
    }

    public interface IAtomicAction<in T1, in T2>
    {
       public void Invoke(T1 args1, T2 args2);
    }
    
    public interface IAtomicAction<in T1, in T2, in T3>
    {
       public void Invoke(T1 args1, T2 args2, T3 args3);
    }
}