using System;
using Sirenix.OdinInspector;

namespace AtomicFramework.AtomicStructures
{
    [Serializable]
    public sealed class AtomicFunction<T> : IAtomicFunction<T>
    {
        private Func<T> _func = null;

        [ShowInInspector, ReadOnly]
        public T CurrentValue => _func != null ? _func.Invoke() : default;

        private AtomicFunction() {}

        public AtomicFunction(Func<T> func)
        {
            _func = func;
        }

        public T Invoke()
        {
            return _func != null ? _func.Invoke() : default;
        }
    }

    [Serializable]
    public sealed class AtomicFunction<T, R> : IAtomicFunction<T, R>
    {
        private Func<T, R> _func = null;

        private AtomicFunction() {}

        public AtomicFunction(Func<T, R> func)
        {
            _func = func;
        }

        [Button]
        public R Invoke(T args)
        {
            return _func.Invoke(args);
        }
    }
}