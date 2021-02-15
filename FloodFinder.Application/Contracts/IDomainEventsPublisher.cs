using System.Security.Claims;
using System.Threading.Tasks;
using FloodFinder.Core.Shared;

namespace FloodFinder.Application.Contracts
{
  public interface IDomainEventsPublisher
  {
    Task Publish(DomainEvent domainEvent);
  }

  public interface ICurrentUserService
  {
    int GetCurrentUserId();
    ClaimsPrincipal GetCurrentUserPrincipal();
  }
}