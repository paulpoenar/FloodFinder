using System.Threading;
using System.Threading.Tasks;
using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FloodFinder.Application.Contracts
{
  public interface IApplicationDbContext
  {
    DbSet<Enquiry> Enquiry { get; set; }
    DbSet<County> County { get; set; }
    DbSet<FloodArea> FloodArea { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    void RollbackTransaction();
  }
}