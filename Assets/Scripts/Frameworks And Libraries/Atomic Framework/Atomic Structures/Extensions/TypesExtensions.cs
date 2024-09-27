using System;

namespace AtomicFramework.AtomicStructures
{
    public static class TypesExtensions
    {
        public static T Let<T>(this T it, Action<T> let)
        {
            let.Invoke(it);
            return it;
        }
        
        public static AtomicValue<T> AsValue<T>(this T it)
        {
            return new AtomicValue<T>(it);
        }

        public static AtomicVariable<T> AsVariable<T>(this T it)
        {
            return new AtomicVariable<T>(it);
        }

        public static AtomicFunction<R> AsFunction<T, R>(this T it, Func<T, R> func)
        {
            return new AtomicFunction<R>(() => func.Invoke(it));
        }

        public static AtomicFunction<bool> AsNot(this IAtomicValue<bool> it)
        {
            return new AtomicFunction<bool>(() => !it.CurrentValue);
        }
    }
}