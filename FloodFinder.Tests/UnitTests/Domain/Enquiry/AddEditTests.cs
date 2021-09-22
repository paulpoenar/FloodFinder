using System.Linq;
using FloodFinder.Tests.Helpers.DataBuilders;
using FluentAssertions;
using Xunit;

namespace FloodFinder.Tests.UnitTests.Domain.Enquiry
{
  public class AddEditTests
  {
    [Fact]
    public void Create_ShouldSucceed()
    {
      var county = CountyBuilder.Create();
      var enquiry = Core.Entities.Enquiry.CreateFrom(county, 1).Data;
      enquiry.Should().NotBe(null);
      enquiry.County.Should().Be(county);
      enquiry.UserId.Should().Be(1);
    }

    [Fact]
    public void Create_ShouldFail()
    {
      var county = CountyBuilder.Create();
      var enquiry = Core.Entities.Enquiry.CreateFrom(county, 1).Data;
      enquiry.Should().NotBe(null);
      enquiry.County.Should().Be(county);
    }

    [Fact]
    public void AddFloodWarning_ShouldAddToList()
    {
      var enquiry = EnquiryBuilder.Create();

      var floodWarning = FloodWarningBuilder.Create();

      enquiry.AddFloodWarning(floodWarning);
      enquiry.FloodWarnings.Should().NotBeEmpty();
      enquiry.FloodWarnings.Last().Should().Be(floodWarning);
    }

  }
}