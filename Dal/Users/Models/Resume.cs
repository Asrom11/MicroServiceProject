using System.ComponentModel.DataAnnotations.Schema;
using Core.Dal.Base;


namespace IdentityServerDal.Roles.Models;

public class Resume: BaseEntityDal<Guid>
{
    [ForeignKey("users")]
    public  Guid UserId { get; set; }
    
    public required int Experience { get; set; }
    
    public required string Skills { get; set; }
    
    public required Education EducationLevel { get; set; }
    
    public required DateTime UpdateDateTime { get; set; }
    
    public UserDal UserDal { get; set; }
}


public enum Education
{
    None,
    School,
    Bachelor,
    Master,
    Doctorate
}