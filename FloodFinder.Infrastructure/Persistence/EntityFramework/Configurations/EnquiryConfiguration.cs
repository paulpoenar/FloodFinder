using System.Runtime.CompilerServices;
using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodFinder.Infrastructure.Persistence.EntityFramework.Configurations
{
  public class EnquiryConfiguration
    : IEntityTypeConfiguration<Enquiry>
  {
    public void Configure(EntityTypeBuilder<Enquiry> configuration)
    {
      configuration
        .HasMany(x => x.FloodWarnings)
        .WithOne(x => x.Enquiry)
        .HasForeignKey(x => x.EnquiryId);

      var navigation = configuration.Metadata.FindNavigation(nameof(Enquiry.FloodWarnings));
      navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
  }
}