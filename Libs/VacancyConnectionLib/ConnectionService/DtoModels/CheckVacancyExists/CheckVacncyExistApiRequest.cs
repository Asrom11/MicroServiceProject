namespace VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;

public class CheckVacncyExistApiRequest
{
    public required Guid VacancyId { get; init; }
}