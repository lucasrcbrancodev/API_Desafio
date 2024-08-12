using API_Desafio.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Desafio.Application.Data;
public interface IApplicationDbContext
{
    DbSet<User> User { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}