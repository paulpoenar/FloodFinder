using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using FloodFinder.Core.Entities;
using FloodFinder.Core.Shared;
using FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations;
using FloodFinder.Infrastructure.Security.Identity;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IPersistedGrantDbContext, IApplicationDbContext
  {
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
    private readonly IDomainEventsPublisher _eventsPublisher;
    private readonly ICurrentUserService _currentUser;

    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    public DbSet<PersistedGrant> PersistedGrants { get; set; }

    public DbSet<Enquiry> Enquiry { get; set; }
    public DbSet<County> County { get; set; }
    public DbSet<FloodArea> FloodArea { get; set; }

    private IDbContextTransaction _currentTransaction;

    public ApplicationDbContext(
      DbContextOptions options,
      IOptions<OperationalStoreOptions> operationalStoreOptions, IDomainEventsPublisher eventsPublisher, ICurrentUserService currentUser) : base(options)
    {
      _operationalStoreOptions = operationalStoreOptions;
      _eventsPublisher = eventsPublisher;
      _currentUser = currentUser;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.ApplyBaseEntityConfiguration();
      builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
      builder.ApplyConfiguration(new CountyConfiguration());
      builder.ApplyConfiguration(new EnquiryConfiguration());
      builder.ApplyConfiguration(new EnquiryFloodWarningConfiguration());
      builder.ApplyConfiguration(new FloodAreaConfiguration());
    }
  


    Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
      foreach (var entry in ChangeTracker.Entries<DomainEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.Created = DateTime.UtcNow;
            entry.Entity.CreatedBy = _currentUser.GetCurrentUserId();
            break;
          case EntityState.Detached:
            break;
          case EntityState.Unchanged:
            break;
          case EntityState.Deleted:
            break;
          case EntityState.Modified:
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      await DispatchEvents();

      var result = await base.SaveChangesAsync(cancellationToken);

      return result;
    }
    
    public async Task BeginTransactionAsync()
    {
      if (_currentTransaction != null)
      {
        return;
      }

      _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
      try
      {
        await SaveChangesAsync().ConfigureAwait(false);

        _currentTransaction?.Commit();
      }
      catch
      {
        RollbackTransaction();
        throw;
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }

    public void RollbackTransaction()
    {
      try
      {
        _currentTransaction?.Rollback();
      }
      finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }

    private async Task DispatchEvents()
    {
      var entities = ChangeTracker
        .Entries<DomainEntity>()
        .Select(x => x.Entity)
        .Where(x => x.DomainEvents != null && x.DomainEvents.Any());

      foreach (var entity in entities.ToList())
      {
        foreach (var @event in entity.DomainEvents)
        {
          await _eventsPublisher.Publish(@event);
        }
        entity.ClearDomainEvents();
      }
    }
  }
}
