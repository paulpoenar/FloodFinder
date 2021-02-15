using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using FloodFinder.Application.Shared.Exceptions;
using FloodFinder.Application.Shared.Models;
using FloodFinder.Core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FloodFinder.Application.UseCases.Enquiry
{
  public class LogEnquiryCommand
  {
    public class Request: IRequest<GenericResponseModel>
    {
      public int CountyId { get; set; }
      public ICollection<FloodWarningModel> Items { get; set; }

      public class FloodWarningModel
      {
        [JsonPropertyName("@id")]
        public string Url { get; set; }
        public string Description { get; set; }
        public string EaAreaName { get; set; }
        public string EaRegionName { get; set; }
        public FloodAreaModel FloodArea { get; set; }

        [JsonPropertyName("floodAreaID")]
        public string FloodAreaUrl { get; set; }
        public bool IsTidal { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public int SeverityLevel { get; set; }
        public DateTime TimeMessageChanged { get; set; }
        public DateTime TimeRaised { get; set; }
        public DateTime TimeSeverityChanged { get; set; }

        public EnquiryFloodWarning ToFloodWarning(FloodArea floodArea)
        {
          return new EnquiryFloodWarning(floodArea, Url, Description, EaAreaName, EaRegionName, IsTidal, Message,
            Severity, SeverityLevel, TimeMessageChanged, TimeRaised, TimeSeverityChanged);
        }

        public class FloodAreaModel
        {
          [JsonPropertyName("@id")]
          public string Url { get; set; }
          public string County { get; set; }
          public string Notation { get; set; }
          public string Polygon { get; set; }
          public string RiverOrSea { get; set; }

          public FloodArea ToFloodArea()
          {
            return new FloodArea(Url, County, Notation, Polygon, RiverOrSea);
          }
        }
      }
      
    }

    public class Validator : AbstractValidator<Request>
    {
      public Validator()
      {
        RuleFor(x => x.CountyId).GreaterThan(0);
      }
    }

    public class CommandHandler : IRequestHandler<Request, GenericResponseModel>
    {
      private readonly IApplicationDbContext _context;

      public CommandHandler(IApplicationDbContext context)
      {
        _context = context;
      }
      
      public async Task<GenericResponseModel> Handle(Request message, CancellationToken token)
      {
        var county = await _context.County.Where(x => x.Id == message.CountyId).SingleOrDefaultAsync(token);

        if (county == null)
        {
          throw new NotFoundException(nameof(County), message.CountyId);
        }

        var enquiry = new Core.Entities.Enquiry(county);

        if (message.Items!= null)
        {
          foreach (var item in message.Items)
          {
            var floodArea = await _context.FloodArea.FirstOrDefaultAsync(x => x.Url == item.FloodAreaUrl, cancellationToken: token);

            if (floodArea == null)
            {
              floodArea = item.FloodArea.ToFloodArea();
            }

            var warning = item.ToFloodWarning(floodArea);
            enquiry.AddFloodWarning(warning);
          }
        }
        
        await _context.Enquiry.AddAsync(enquiry, token);
        
        await _context.SaveChangesAsync(token);

        return GenericResponse.Success();
      }
    }
  }
}