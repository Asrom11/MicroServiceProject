using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture;

public class ApplicationDbContext: DbContext
{
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<VacancyFeedback> VacancyFeedbacks { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}