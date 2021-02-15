using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations
{
  public class EnquiryFloodWarningConfiguration
    : IEntityTypeConfiguration<EnquiryFloodWarning>
  {
    public void Configure(EntityTypeBuilder<EnquiryFloodWarning> configuration)
    {
      configuration
        .HasOne(x => x.FloodArea)
        .WithMany(x => x.Responses)
        .HasForeignKey(x => x.FloodAreaId);

      configuration.Property(o => o.Severity)
        .HasMaxLength(150)
        .IsRequired();
    }
  }
}