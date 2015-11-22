using System;

namespace GuardedDisposables
{
    internal sealed class GuardedDisposableException : Exception
    {
        public Exception ExecutionException { get; set; }
        public Exception DisposeException { get; set; }

        public GuardedDisposableException(Exception executionException, Exception disposeException)
        {
            ExecutionException = executionException;
            DisposeException = disposeException;
        }

        public override string Message
        {
            get { return ExecutionException.Message + Environment.NewLine + DisposeException.Message; }
        }

        public override string StackTrace
        {
            get { return ExecutionException.StackTrace + Environment.NewLine + DisposeException.StackTrace; }
        }
    }
}
