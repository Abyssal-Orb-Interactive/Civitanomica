using System;
using System.Linq;

namespace AtomicFramework.AtomicStructures
{
    internal static class DisposeUtil
    {
        public static void Dispose(ref Action action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            action = delegates.Aggregate(action, (current, @delegate) => current - (Action)@delegate);

            action = null;
        }

        public static void Dispose<T>(ref T @delegate) where T : Delegate
        {
            if (@delegate == null)
            {
                return;
            }

            foreach (var value in @delegate.GetInvocationList())
            {
                @delegate = (T) Delegate.Remove(@delegate, value);
            }

            @delegate = null;
        }

        public static void Dispose<T>(ref Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            action = delegates.Aggregate(action, (current, @delegate) => current - (Action<T>)@delegate);

            action = null;
        }
    }
}