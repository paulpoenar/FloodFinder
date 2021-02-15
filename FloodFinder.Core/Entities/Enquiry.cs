using System.Collections.Generic;
using System.ComponentModel;
using FloodFinder.Core.Shared;

namespace FloodFinder.Core.Entities
{
  public partial class Enquiry: DomainEntity
  {
    public int CountyId { get; private set; }
    public County County { get; private set; }

    private readonly List<EnquiryFloodWarning> _floodWarnings;
    public IReadOnlyCollection<EnquiryFloodWarning> FloodWarnings => _floodWarnings;

    private Enquiry()
    {
      
    }

    public Enquiry(County county)
    {
      County = county;
      _floodWarnings = new List<EnquiryFloodWarning>();
    }

    public void AddFloodWarning(EnquiryFloodWarning warning)
    {
      _floodWarnings.Add(warning);
    }
  }
}