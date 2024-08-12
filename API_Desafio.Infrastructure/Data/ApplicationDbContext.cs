using API_Desafio.Application.Data;
using API_Desafio.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Desafio.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; }
}
