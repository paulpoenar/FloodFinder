using FloodFinder.Core.Entities;

namespace FloodFinder.Tests.Helpers.DataBuilders
{
  public class FloodAreaBuilder
  {
    public static FloodArea Create()
    {
      var enquiry = new FloodArea("aaad", "aaad", "aaad", "aaad", "aaad");
      return enquiry;
    }
  }
}