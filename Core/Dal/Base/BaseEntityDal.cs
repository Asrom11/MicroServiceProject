using System.ComponentModel.DataAnnotations;

namespace Core.Dal.Base;

public class BaseEntityDal<T>
{
    [Key]
    public T Id { get; set; }
}