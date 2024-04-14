namespace Domain.Interfaces;

public interface ICheckEntity
{
    Task CheckEntityAsync(Guid entityId);
}