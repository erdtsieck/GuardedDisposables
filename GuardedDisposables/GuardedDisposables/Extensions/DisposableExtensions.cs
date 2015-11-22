using System;

namespace GuardedDisposables
{
    public static class DisposableExtensions
    {
        public static IGuardedDisposableWrapper<TGuardedDisposable> Guard<TGuardedDisposable>(this TGuardedDisposable disposable) 
            where TGuardedDisposable : IDisposable
        {
            return new GuardedDisposableWrapper<TGuardedDisposable>(disposable);
        }
    }
}
