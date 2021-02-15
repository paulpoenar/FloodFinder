using System.Collections.Generic;

namespace FloodFinder.Application.Shared.Models
{
  /// <summary>
  /// Used to return a generic response for APIs
  /// </summary>
  public class GenericResponseModel<T> : GenericResponseModel
  {
    public T Data { get; set; }
  }

  /// <summary>
  /// Used to return a generic response for APIs
  /// </summary>
  public class GenericResponseModel
  {
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
  }
}