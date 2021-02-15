using FloodFinder.Tests.DataBuilders;
using FluentAssertions;
using Xunit;

namespace FloodFinder.Tests.UnitTests.Enquiry
{
  public class AddEditTests
  {
    [Fact]
    public void Add()
    {
      var county = CountyBuilder.Create();
      var entity = new Core.Entities.Enquiry(county);
      entity.Should().NotBe(null);
      entity.County.Should().Be(county);
    }
  }
}