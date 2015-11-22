using System;

namespace GuardedDisposables
{
    internal sealed class GuardedDisposableWrapper<TGuardedDisposable> 
        : IGuardedDisposableWrapper<TGuardedDisposable> 
        where TGuardedDisposable : IDisposable
    {
        private readonly TGuardedDisposable guardedDisposable;
        private Exception executionException;
        private Exception disposableException;

        public GuardedDisposableWrapper(TGuardedDisposable guardedDisposable)
        {
            this.guardedDisposable = guardedDisposable;
        }

        public void Execute(Action<TGuardedDisposable> action)
        {
            try
            {
                action(guardedDisposable);
            }
            catch (Exception e)
            {
                executionException = e;
            }
        }

        public TResult Execute<TResult>(Func<TGuardedDisposable, TResult> action)
        {
            try
            {
                return action(guardedDisposable);
            }
            catch (Exception e)
            {
                executionException = e;
            }
            return default(TResult);
        }

        public void Dispose()
        {
            try
            {
                guardedDisposable.Dispose();
            }
            catch (Exception e)
            {
                disposableException = e;
            }
            finally
            {
                if (executionException != null || disposableException != null)
                {
                    throw new GuardedDisposableException(executionException, disposableException);
                }
            }
        }
    }
}
