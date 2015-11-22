# GuardedDisposable
Since IDisposables are usually used in a using block, thereby creating a try-finally which might swallow an exception if the IDisposable's implementation throws an Exception in the Dispose method.
