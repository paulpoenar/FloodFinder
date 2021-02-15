using FloodFinder.Application.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FloodFinder.Helpers
{
  public static class GenericResponseExtensions
  {
    public static IActionResult ToActionResult(this GenericResponseModel response)
    {
      if (response.Success)
      {
        return new OkObjectResult(response);
      }

      return new BadRequestObjectResult(response);
    }
  }
}