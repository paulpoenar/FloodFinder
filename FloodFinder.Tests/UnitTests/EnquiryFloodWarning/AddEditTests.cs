using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloodFinder.Tests.DataBuilders;
using FluentAssertions;
using Xunit;

namespace FloodFinder.Tests.UnitTests.EnquiryFloodWarning
{
  public class AddEditTests
  {
    [Fact]
    public void Add()
    {
      var url = "agmehjxw1";
      var description = "agmehjxw2";
      var eaAreaName = "agmehjxw3";
      var eaRegionName = "agmehjxw4";
      var floodAreaId = 879;
      var isTidal = true;
      var message = "agmehjxw5";
      var severity = "agmehjxw6";
      var severityLevel = 879;
      var timeMessageChanged = new DateTime(2020,1 ,1);
      var timeRaised = new DateTime(2020, 1, 2);
      var timeSeverityChanged = new DateTime(2020, 1, 3);

      var floodArea = FloodAreaBuilder.Create();
      var entity = new Core.Entities.EnquiryFloodWarning(floodArea,url, description, eaAreaName, eaRegionName, isTidal, message, severity, severityLevel, timeMessageChanged, timeRaised, timeSeverityChanged);
      entity.Should().NotBe(null);
      entity.Url.Should().Be(url);
      entity.Description.Should().Be(description);
      entity.EaAreaName.Should().Be(eaAreaName);
      entity.EaRegionName.Should().Be(eaRegionName);
      entity.FloodArea.Should().Be(floodArea);
      entity.IsTidal.Should().Be(isTidal);
      entity.Message.Should().Be(message);
      entity.Severity.Should().Be(severity);
      entity.SeverityLevel.Should().Be(severityLevel);
      entity.TimeMessageChanged.Should().Be(timeMessageChanged);
      entity.TimeRaised.Should().Be(timeRaised);
      entity.TimeSeverityChanged.Should().Be(timeSeverityChanged);

    }
  }
}
