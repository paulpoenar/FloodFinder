using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using FloodFinder.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting.Internal;
using Moq;
using Respawn;

namespace FloodFinder.Tests.IntegrationTests
{
  public class SliceFixture<TContext> where TContext : DbContext, IApplicationDbContext
  {
    private static readonly Checkpoint _checkpoint;
    private static readonly IConfigurationRoot _configuration;
    private static readonly IServiceScopeFactory _scopeFactory;
    private static int _currentUserId;

    static SliceFixture()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", true, true)
          .AddEnvironmentVariables();

      _configuration = builder.Build();
      
      var startup = new Startup(_configuration);
      var services = new ServiceCollection();

      services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
        w.EnvironmentName == "Development" &&
        w.ApplicationName == "FloodFinder"));

      services.AddLogging();

      startup.ConfigureServices(services);
      SetMocks(services);
      var provider = services.BuildServiceProvider();
      RunContextMigrations(provider);
      _scopeFactory = provider.GetService<IServiceScopeFactory>();
      _checkpoint = new Checkpoint()
      { 
        TablesToIgnore = new[] { "__EFMigrationsHistory", nameof(County)  }
      };
    }

    public static async Task ResetCheckpoint()
    {
      var connString = _configuration.GetConnectionString("DefaultConnection");
      await _checkpoint.Reset(connString);
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
      using var scope = _scopeFactory.CreateScope();
      var dbContext = scope.ServiceProvider.GetService<TContext>();

      try
      {
        await dbContext.BeginTransactionAsync().ConfigureAwait(false);

        var result = await action(scope.ServiceProvider).ConfigureAwait(false);

        await dbContext.CommitTransactionAsync().ConfigureAwait(false);

        return result;
      }
      catch (Exception)
      {
        dbContext.RollbackTransaction();
        throw;
      }
    }

    public static Task<T> ExecuteDbContextAsync<T>(Func<TContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TContext>()));

    public static Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
    {
      return ExecuteDbContextAsync(db =>
      {
        db.Set<TEntity>().Add(entity);

        return db.SaveChangesAsync();
      });
    }

    public static Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
      return ExecuteScopeAsync(sp =>
      {
        var mediator = sp.GetService<IMediator>();

        return mediator.Send(request);
      });
    }

    private static void RunContextMigrations(IServiceProvider services)
    {
      using (var context = services.GetRequiredService<TContext>())
      {
        context.Database.Migrate();
      }
    }

    private static void SetMocks(ServiceCollection services)
    {
      var currentUserServiceDescriptor = services.FirstOrDefault(d =>
        d.ServiceType == typeof(ICurrentUserService));

      services.Remove(currentUserServiceDescriptor);

      // Register testing version
      services.AddTransient(provider =>
        Mock.Of<ICurrentUserService>(s => s.GetCurrentUserId() == _currentUserId));

    }
  }
}
