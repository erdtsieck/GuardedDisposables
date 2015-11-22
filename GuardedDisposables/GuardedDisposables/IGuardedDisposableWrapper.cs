using System;

namespace GuardedDisposables
{
    public interface IGuardedDisposableWrapper<out TGuardedDisposable> : IDisposable
        where TGuardedDisposable : IDisposable
    {
        void Execute(Action<TGuardedDisposable> action);

        TResult Execute<TResult>(Func<TGuardedDisposable, TResult> action);
    }
}
