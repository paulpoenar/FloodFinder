using System.Collections.Generic;
using FloodFinder.Core.Shared;

namespace FloodFinder.Application.Shared.Models
{
  /// <summary>
  /// Used to return a generic response for APIs
  /// </summary>
  public class ApplicationResponse<T> : ApplicationResponse
  {
    public T Data { get; set; }
  }

  /// <summary>
  /// Used to return a generic response for APIs
  /// </summary>
  public class ApplicationResponse
  {
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
    public static ApplicationResponse Succeeded()
    {
      return new ApplicationResponse() { Success = true };
    }

    public static ApplicationResponse Failed(string error)
    {
      return new ApplicationResponse() { Success = false, Errors = new List<string>() { error } };
    }

  }

  public static class DomainActionResponseExtensions
  {
    public static ApplicationResponse ToApplicationResponseModel(this DomainResponse response)
    {
      return !response.Success ? ApplicationResponse.Failed(response.Error) : ApplicationResponse.Succeeded();
    }
  }
}