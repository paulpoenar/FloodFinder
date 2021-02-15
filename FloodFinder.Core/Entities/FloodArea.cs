using System.Collections.Generic;
using FloodFinder.Core.Shared;
using Microsoft.VisualBasic;

namespace FloodFinder.Core.Entities
{
  public partial class FloodArea : DomainEntity
  {
    public string Url { get; private set; }
    public string County { get; private set; }
    public string Notation { get; private set; }
    public string Polygon { get; private set; }
    public string RiverOrSea { get; private set; }

    public ICollection<EnquiryFloodWarning> Responses { get; set; }

    private FloodArea() { }

    public FloodArea(string url, string county, string notation, string polygon, string riverOrSea)
    {
      Url = url;
      County = county;
      Notation = notation;
      Polygon = polygon;
      RiverOrSea = riverOrSea;
    }
  }
}