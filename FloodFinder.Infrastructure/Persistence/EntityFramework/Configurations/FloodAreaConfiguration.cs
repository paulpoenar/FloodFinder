using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations
{
  public class FloodAreaConfiguration
    : IEntityTypeConfiguration<FloodArea>
  {
    public void Configure(EntityTypeBuilder<FloodArea> configuration)
    {
      configuration.Property(o => o.County)
        .HasMaxLength(100)
        .IsRequired();
    }
  }
}