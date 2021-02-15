using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Nito.AsyncEx;
using Xunit;

namespace FloodFinder.Tests.IntegrationTests
{
  public abstract class IntegrationTestBase<TContext> : IAsyncLifetime where TContext : DbContext, IApplicationDbContext
  {
    private static readonly AsyncLock _mutex = new AsyncLock();

    private static bool _initialized;

    public virtual async Task InitializeAsync()
    {
      if (_initialized)
        return;

      using (await _mutex.LockAsync())
      {
        if (_initialized)
          return;

        await SliceFixture<TContext>.ResetCheckpoint();

        _initialized = true;
      }
    }

    public virtual Task DisposeAsync() => Task.CompletedTask;
  }
}