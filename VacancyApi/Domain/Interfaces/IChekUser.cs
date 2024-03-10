namespace Services.Interfaces;

public interface IChekUser
{
    Task CheckUserExistAsync(Guid userId);
}