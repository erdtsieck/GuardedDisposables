# GuardedDisposable
Since IDisposables are usually used in a using block, they create a try-finally which might swallow an exception if the IDisposable's implementation throws an Exception in the Dispose method. GuardedDisposables ensures those Exceptions will not get lost.

# Usage
```C#
[TestMethod]
public void DirtyDisposableWrapperThrowsBothExceptions()
{
    try
    {
        // Arrange 
        var mock = new Mock<IThirdPartyDisposable>();
        mock.Setup(d => d.Foo()).Throws(new ThirdPartyDisposableFooException());
        mock.Setup(d => d.Dispose()).Throws(new ThirdPartyDisposableDisposeException());

        using (var guardedDisposableWrapper = mock.Object.Guard())
        {
            // Act
            guardedDisposableWrapper.Execute(d => d.Foo());
        }
    }
    catch (GuardedDisposableException dirtyException)
    {
        // Assert
        Assert.IsNotNull(dirtyException.ExecutionException);
        Assert.IsNotNull(dirtyException.DisposeException);
    }
}
```
