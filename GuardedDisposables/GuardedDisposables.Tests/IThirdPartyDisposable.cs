using System;

namespace GuardedDisposables.Tests
{
    public interface IThirdPartyDisposable : IDisposable
    {
        void Foo();
    }
}
