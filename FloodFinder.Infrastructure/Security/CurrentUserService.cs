using System;
using System.Linq;
using System.Security.Claims;
using FloodFinder.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace FloodFinder.Infrastructure.Security
{
  public class CurrentUserService: ICurrentUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public int GetId()
    {
      var currentUser = _httpContextAccessor.HttpContext.User;

      if (!currentUser.Claims.Any()) throw new NotImplementedException();

      var userId = currentUser.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

      return Convert.ToInt32(userId);
    }

    public ClaimsPrincipal GetCurrentUserPrincipal()
    {
      return _httpContextAccessor.HttpContext.User;
    }

  }
}