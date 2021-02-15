using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations
{
  public class CountyConfiguration
    : IEntityTypeConfiguration<County>
  {
    public void Configure(EntityTypeBuilder<County> configuration)
    {
      configuration.Property(o => o.Name)
        .HasMaxLength(100)
        .IsRequired();
    }
  }
}