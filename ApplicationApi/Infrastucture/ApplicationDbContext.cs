using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture;

public class ApplicationDbContext: DbContext
{
    public DbSet<VacancyApplication> VacancyApplications { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}