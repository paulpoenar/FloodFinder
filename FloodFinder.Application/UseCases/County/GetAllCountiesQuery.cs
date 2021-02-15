using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FloodFinder.Application.Contracts;
using FloodFinder.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FloodFinder.Application.UseCases.County
{
  public class GetAllCountiesQuery
  {
    public class Request : IRequest<GenericResponseModel<List<Model>>>
    { }

    public class Model
    {
      public int Id { get; set; }
      public string Name { get; set; }

    }
    
    public class QueryHandler : IRequestHandler<Request, GenericResponseModel<List<Model>>>
    {
      private readonly IApplicationDbContext _context;

      public QueryHandler(IApplicationDbContext context)
      {
        _context = context;
      }


      public async Task<GenericResponseModel<List<Model>>> Handle(Request message, CancellationToken token)
      {
        var records = await _context.County.AsNoTracking()
          .Select(x=> new Model()
          {
            Id = x.Id,
            Name = x.Name
          }).ToListAsync(token);

        return GenericResponse.Success(records);
      }
    }
  }
}