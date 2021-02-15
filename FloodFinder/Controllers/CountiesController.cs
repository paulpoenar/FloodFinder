using System.Threading.Tasks;
using FloodFinder.Application.UseCases.County;
using FloodFinder.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FloodFinder.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class CountiesController : ControllerBase
  {
    private readonly IMediator _mediator;

    public CountiesController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var query = await _mediator.Send(new GetAllCountiesQuery.Request());
      return query.ToActionResult();
    }
  }
}