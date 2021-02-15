using FloodFinder.Core.Entities;

namespace FloodFinder.Tests.DataBuilders
{
  public class EnquiryBuilder
  {
    public static Enquiry Create()
    {
      var enquiry = new Enquiry(CountyBuilder.Create());
      return enquiry;
    }
  }
}