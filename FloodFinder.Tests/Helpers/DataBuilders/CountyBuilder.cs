using FloodFinder.Core.Entities;

namespace FloodFinder.Tests.Helpers.DataBuilders
{
  public class CountyBuilder
  {
    public static County Create(string name = "testCounty")
    {
      var county = new County();
      county.SetProperty(x=> x.Name, name);
      return county;
    }
  }
}