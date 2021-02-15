using System.Linq;
using System.Reflection;
using FloodFinder.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations
{
  public static class BaseEntityConfiguration
  {
    static void Configure<TEntity>(ModelBuilder modelBuilder)
      where TEntity : DomainEntity
    {
      modelBuilder.Entity<TEntity>(builder =>
      {
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
      });
    }

    public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder)
    {
      var method = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
        .Single(m => m.Name == nameof(Configure));

      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        if (entityType.ClrType.IsSubclassOf(typeof(DomainEntity)))
          method.MakeGenericMethod(entityType.ClrType).Invoke(null, new[] { modelBuilder });
      }
      return modelBuilder;
    }

  }
}