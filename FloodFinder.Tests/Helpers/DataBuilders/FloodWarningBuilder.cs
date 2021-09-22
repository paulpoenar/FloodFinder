using System;
using FloodFinder.Core.Entities;

namespace FloodFinder.Tests.Helpers.DataBuilders
{
  public class FloodWarningBuilder
  {
    public static EnquiryFloodWarning Create()
    {
      var county = new EnquiryFloodWarning(FloodAreaBuilder.Create(), "agmehjxw", "agmehjxw", "agmehjxw",
        "agmehjxw",true, "agmehjxw", "agmehjxw", 1,
        DateTime.Now, DateTime.Now, DateTime.Now);
    
      return county;
    }
  }
}