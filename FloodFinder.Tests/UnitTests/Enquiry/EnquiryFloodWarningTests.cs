using System.Linq;
using FloodFinder.Tests.DataBuilders;
using FluentAssertions;
using Xunit;

namespace FloodFinder.Tests.UnitTests.Enquiry
{
  public class EnquiryFloodWarningTests
  {
    [Fact]
    public void Add()
    {
      var county = CountyBuilder.Create();
      var entity = new Core.Entities.Enquiry(county);

      var floodWarning = FloodWarningBuilder.Create();

      entity.AddFloodWarning(floodWarning);
      entity.FloodWarnings.Should().NotBeEmpty();
      entity.FloodWarnings.Last().Should().Be(floodWarning);
    }
  }
}