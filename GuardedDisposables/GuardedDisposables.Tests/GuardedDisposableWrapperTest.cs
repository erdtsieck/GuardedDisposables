using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GuardedDisposables.Tests
{
    [TestClass]
    public class GuardedDisposableWrapperTest
    {
        [TestMethod]
        [ExpectedException(typeof(ThirdPartyDisposableDisposeException))]
        public void DirtyDisposableOnlyThrowsFinallyException()
        {
            // Arrange 
            var mock = new Mock<IThirdPartyDisposable>();
            mock.Setup(d => d.Foo()).Throws(new ThirdPartyDisposableFooException());
            mock.Setup(d => d.Dispose()).Throws(new ThirdPartyDisposableDisposeException());
            
            using (var mockObject = mock.Object)
            {
                // Act
                mockObject.Foo();
            }

            // Assert: only ThirdPartyDisposableDisposeException is thrown,
            // The Exception thrown in Foo is swallowed 
        }

        [TestMethod]
        [ExpectedException(typeof(GuardedDisposableException))]
        public void DirtyDisposableWrapperThrowsDirtyDisposableException()
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

            // Assert: throws DirtyDisposableException
        }

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

                Assert.IsInstanceOfType(dirtyException.DisposeException, typeof(ThirdPartyDisposableDisposeException));
                Assert.IsInstanceOfType(dirtyException.ExecutionException, typeof(ThirdPartyDisposableFooException));
            }
        }
    }
}
