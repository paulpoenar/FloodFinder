using System.Collections.Generic;
using System.ComponentModel;
using FloodFinder.Core.Shared;

namespace FloodFinder.Core.Entities
{
  public partial class Enquiry: DomainEntity
  {
    public int CountyId { get; private set; }
    public int UserId { get; private set; }
    public County County { get; private set; }

    private List<EnquiryFloodWarning> _floodWarnings;
    public IReadOnlyCollection<EnquiryFloodWarning> FloodWarnings => _floodWarnings;

    private Enquiry()
    {
      
    }

    public static DomainResponse<Enquiry> CreateFrom(County county, int userId)
    {
      if (county==null)
      {
        return DomainResponse.Failed("Unable to create. County is null", (Enquiry)null);
      }

      var enquiry = new Enquiry()
      {
        County = county,
        _floodWarnings = new List<EnquiryFloodWarning>(),
        UserId = userId
      };

      return DomainResponse.Succeeded(enquiry);

    }

    public void AddFloodWarning(EnquiryFloodWarning warning)
    {
      _floodWarnings.Add(warning);
    }
  }
}