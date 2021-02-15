using System;
using FluentAssertions;
using Xunit;

namespace FloodFinder.Tests.UnitTests.FloodArea
{
  public class AddEditTests
  {
    [Fact]
    public void Add()
    {
      var url = "kyidpdqw1";
      var county = "kyidpdqw2";
      var notation = "kyidpdqw3";
      var polygon = "kyidpdqw4";
      var riverOrSea = "kyidpdqw5";
      var entity = new Core.Entities.FloodArea(url, county, notation, polygon, riverOrSea);
      entity.Should().NotBe(null);
      entity.Url.Should().Be(url);
      entity.County.Should().Be(county);
      entity.Notation.Should().Be(notation);
      entity.Polygon.Should().Be(polygon);
      entity.RiverOrSea.Should().Be(riverOrSea);
    }
  }
}