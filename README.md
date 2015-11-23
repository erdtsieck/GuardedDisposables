# GuardedDisposable
Since IDisposables are usually used in an using block, they create a try-finally which might swallow an exception if the IDisposable's implementation throws an Exception in the Dispose method. GuardedDisposables ensures those Exceptions will not get lost.

# Usage
```C#
try
{
    using (var guardedDisposableWrapper = new ThirdPartyDisposable().Guard())
    {
        guardedDisposableWrapper.Execute(d => d.Foo());
    }
}
catch (GuardedDisposableException guardedDisposableException)
{
    HandleExeption(guardedDisposableException.ExecutionException);
    HandleExeption(guardedDisposableException.DisposeException);
}
```
