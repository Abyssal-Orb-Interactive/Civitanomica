using System;

namespace ReactiveLibraryFacade
{
    public interface IObserver : IDisposable
    {
        public void OnChange();
    }
    
    public interface IObserver<in T> : IDisposable
    {
        public void OnChange(T value);
    }
    
    public interface IObserver<in T1, in T2> : IDisposable
    {
        public void OnChange(T1 value1, T2 value2);
    }
    
    public interface IObserver<in T1, in T2, in T3> : IDisposable
    {
        public void OnChange(T1 value1, T2 value2, T3 value3);
    }
}