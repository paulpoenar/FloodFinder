using System;
using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using FloodFinder.Application.UseCases.Enquiry;
using FloodFinder.Core.Entities;
using FloodFinder.Tests.Helpers.DataBuilders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FloodFinder.Tests.UnitTests.Application
{
  public class LogEnquiryTests
  {
    [Fact]
    public void LogEnquiryCommand_FloodWarningModel_ToDomainModel()
    {
      var inputModel = new LogEnquiryCommand.Request.FloodWarningModel()
      {
        Url = "agmehjxw1",
        Description = "agmehjxw2",
        EaAreaName = "agmehjxw3",
        EaRegionName = "agmehjxw4",
        IsTidal = true,
        Message = "agmehjxw5",
        Severity = "agmehjxw6",
        SeverityLevel = 879,
        TimeMessageChanged = new DateTime(2020, 1, 1),
        TimeRaised = new DateTime(2020, 1, 2),
        TimeSeverityChanged = new DateTime(2020, 1, 3),
      };

      var floodArea = FloodAreaBuilder.Create();
      
      var entity = inputModel.ToDomainModel(floodArea);
      
      entity.Should().NotBe(null);
      entity.Url.Should().Be(inputModel.Url);
      entity.Description.Should().Be(inputModel.Description);
      entity.EaAreaName.Should().Be(inputModel.EaAreaName);
      entity.EaRegionName.Should().Be(inputModel.EaRegionName);
      entity.FloodArea.Should().Be(floodArea);
      entity.IsTidal.Should().Be(inputModel.IsTidal);
      entity.Message.Should().Be(inputModel.Message);
      entity.Severity.Should().Be(inputModel.Severity);
      entity.SeverityLevel.Should().Be(inputModel.SeverityLevel);
      entity.TimeMessageChanged.Should().Be(inputModel.TimeMessageChanged);
      entity.TimeRaised.Should().Be(inputModel.TimeRaised);
      entity.TimeSeverityChanged.Should().Be(inputModel.TimeSeverityChanged);
    }
  }
}