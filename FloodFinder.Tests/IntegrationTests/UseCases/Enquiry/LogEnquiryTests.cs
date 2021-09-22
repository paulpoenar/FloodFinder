using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloodFinder.Application.UseCases.Enquiry;
using FloodFinder.Infrastructure.Persistence.EntityFramework;
using FloodFinder.Tests.Helpers.DataBuilders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FloodFinder.Tests.IntegrationTests.UseCases.Enquiry
{
  using static SliceFixture<ApplicationDbContext>;

  [Collection("IntegrationTests")]
  [CollectionDefinition("IntegrationTests", DisableParallelization = true)]
  public class LogEnquiryTests : IntegrationTestBase<ApplicationDbContext>
  {
    [Fact]
    public async Task RequestWithNoWarnings_ShouldSucceed()
    {
      await ResetCheckpoint();
      var county = CountyBuilder.Create();

      await InsertAsync(county);

      var command = new LogEnquiryCommand.Request(){CountyId = county.Id};
      
      var response = await SendAsync(command);

      var newRecord = await ExecuteDbContextAsync(db => db.Enquiry
        .Include(x=>x.FloodWarnings)
        .Where(d => d.CountyId == county.Id).SingleOrDefaultAsync());

      newRecord.Should().NotBeNull();
      newRecord.FloodWarnings.Count.Should().Be(0);
    }

    [Fact]
    public async Task ShouldCreateFloodWarningsAndFloodArea()
    {
      var county = CountyBuilder.Create();

      await InsertAsync(county);

      var command = new LogEnquiryCommand.Request() { CountyId = county.Id };
      command.Items = new List<LogEnquiryCommand.Request.FloodWarningModel>()
      {
        new()
        {
          Url = "WarningUrl",
          FloodAreaUrl = "FloodAreaUrl",
          Severity = "high",
          SeverityLevel = 1,
          FloodArea = new LogEnquiryCommand.Request.FloodWarningModel.FloodAreaModel()
          {
            Url = "FloodAreaUrl",
            County = county.Name
          }
        }
      };

      var response = await SendAsync(command);

      var newStockItemOperation = await ExecuteDbContextAsync(db => db.Enquiry
        .Include(x => x.FloodWarnings)
        .Where(d => d.CountyId == county.Id).SingleOrDefaultAsync());

      newStockItemOperation.Should().NotBeNull();
      newStockItemOperation.FloodWarnings.Count.Should().Be(1);
      var newWarning = newStockItemOperation.FloodWarnings.First();
      newWarning.Url.Should().Be(command.Items.First().Url);

    }

    [Fact]
    public async Task ShouldCreateFloodWarningsAndUseExistingFloodArea()
    {
      var county = CountyBuilder.Create();
      await InsertAsync(county);

      var floodArea = FloodAreaBuilder.Create();
      await InsertAsync(floodArea);

      var command = new LogEnquiryCommand.Request() { CountyId = county.Id };
      command.Items = new List<LogEnquiryCommand.Request.FloodWarningModel>()
      {
        new()
        {
          Url = "WarningUrl",
          FloodAreaUrl = floodArea.Url,
          Severity = "high",
          SeverityLevel = 1,
          FloodArea = new LogEnquiryCommand.Request.FloodWarningModel.FloodAreaModel()
          {
            Url = floodArea.Url,
            County = county.Name
          }
        }
      };

      var response = await SendAsync(command);

      var newStockItemOperation = await ExecuteDbContextAsync(db => db.FloodArea
        .Where(d => d.Url == floodArea.Url).SingleOrDefaultAsync());

      newStockItemOperation.Should().NotBeNull();

    }

  }
}