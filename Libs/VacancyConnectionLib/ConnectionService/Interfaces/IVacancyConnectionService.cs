using VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;

namespace VacancyConnectionLib.ConnectionService.Interfaces;

public interface IVacancyConnectionService
{
    Task CheckVacancyExistAsycn(CheckVacncyExistApiRequest vacancyId);
}