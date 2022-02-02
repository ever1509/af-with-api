using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IContext
{
    DbSet<Url> Urls { get; set; }
    DbSet<Click> Clicks { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

