using FloodFinder.Core.Entities;

namespace FloodFinder.Tests.Helpers.DataBuilders
{
  public class EnquiryBuilder
  {
    public static Enquiry Create()
    {
      var enquiry = Enquiry.CreateFrom(CountyBuilder.Create(), 1).Data;
      return enquiry;
    }
  }
}