using System.ComponentModel.DataAnnotations;

namespace Core.Dal.Base;

public record BaseEntityDal<T>
{
    [Key]
    public T Id { get; set; }
}