using IdentityServerDal.Roles.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerDal;

public class ApplicationDbContext: DbContext
{
    public DbSet<UserDal> Users { get; set; }
    public DbSet<RoleDal> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Resume> Resumes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    
}