using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class Request : IRequest<ApplicationResponse>
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

        public EnquiryFloodWarning ToDomainModel(FloodArea floodArea)
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

          public FloodArea ToDomainModel()
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

    public class CommandHandler : IRequestHandler<Request, ApplicationResponse>
    {
      private readonly IApplicationDbContext _context;
      private readonly ICurrentUserService _currentUser;

      public CommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
      {
        _context = context;
        _currentUser = currentUser;
      }

      public async Task<ApplicationResponse> Handle(Request request, CancellationToken token)
      {
        var county = await _context.County
          .Where(x => x.Id == request.CountyId)
          .FirstOrDefaultAsync(token);

        var userId = _currentUser.GetId();

        //try creating an enquiry
        var createEnquiryResult = Core.Entities.Enquiry.CreateFrom(county, userId);

        if (!createEnquiryResult.Success)
        {
          return createEnquiryResult.ToApplicationResponseModel();
        }

        var enquiry = createEnquiryResult.Data;

        if (request.Items != null)
        {
          //store each warning
          foreach (var item in request.Items)
          {
            //get from DB or build from request
            var floodArea = await _context.FloodArea
              .FirstOrDefaultAsync(x => x.Url == item.FloodAreaUrl, token) ?? item.FloodArea.ToDomainModel();
            var warning = item.ToDomainModel(floodArea);
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