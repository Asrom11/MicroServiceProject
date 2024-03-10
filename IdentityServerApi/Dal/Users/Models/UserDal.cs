using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExampleCore.Dal.Base;

namespace IdentityServerDal.Roles.Models;


[Table("users")]
public record UserDal: BaseEntity<Guid>
{
    [Column("name")]
    public required string Name { get; set; }
    
    [Column("email")]
    public required string Email { get; set; }
    
    public required string PaswordHash { get; set; }

    public Resume Resume { get; set; }
    public List<UserRole> UserRole { get; set; }
}