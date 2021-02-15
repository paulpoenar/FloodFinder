using System;
using FloodFinder.Core.Shared;

namespace FloodFinder.Core.Entities
{
  public partial class EnquiryFloodWarning : DomainEntity
  {
    public int EnquiryId { get; private set; }
    public string Url { get; private set; }
    public string Description { get; private set; }
    public string EaAreaName { get; private set; }
    public string EaRegionName { get; private set; }
    public int FloodAreaId { get; private set; }
    public bool IsTidal { get; private set; }
    public string Message { get; private set; }
    public string Severity { get; private set; }
    public int SeverityLevel { get; private set; }
    public DateTime TimeMessageChanged { get; private set; }
    public DateTime TimeRaised { get; private set; }
    public DateTime TimeSeverityChanged { get; private set; }

    public Enquiry Enquiry { get; private set; }
    public FloodArea FloodArea { get; private set; }

    private EnquiryFloodWarning() { }


    public EnquiryFloodWarning(FloodArea floodArea, string url, string description, string eaAreaName, string eaRegionName, bool isTidal, string message, string severity, int severityLevel, DateTime timeMessageChanged, DateTime timeRaised, DateTime timeSeverityChanged)
    {
      Url = url;
      Description = description;
      EaAreaName = eaAreaName;
      EaRegionName = eaRegionName;
      FloodArea = floodArea;
      IsTidal = isTidal;
      Message = message;
      Severity = severity;
      SeverityLevel = severityLevel;
      TimeMessageChanged = timeMessageChanged;
      TimeRaised = timeRaised;
      TimeSeverityChanged = timeSeverityChanged;
    }
  }
}