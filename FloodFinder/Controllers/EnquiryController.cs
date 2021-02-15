using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloodFinder.Application.UseCases.Enquiry;
using FloodFinder.Helpers;
using MediatR;

namespace FloodFinder.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class EnquiryController : ControllerBase
  {
    private readonly IMediator _mediator;

    public EnquiryController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Log(LogEnquiryCommand.Request request)
    {
      var response = await _mediator.Send(request);
      return response.ToActionResult();
    }
  }
}
