using Domain.Interfaces;
using VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;
using VacancyConnectionLib.ConnectionService.Interfaces;

namespace Infrastucture.Connection;

public class CheckVacancy: ICheckVacancy
{
    private readonly IVacancyConnectionService _vacancyConnectionServcie;

    public CheckVacancy(IVacancyConnectionService vacancyConnectionServcie)
    {
        _vacancyConnectionServcie = vacancyConnectionServcie;
    }

    public async Task CheckEntityAsync(Guid vacancyId)
    {
        await _vacancyConnectionServcie.CheckVacancyExistAsycn(new CheckVacncyExistApiRequest()
        {
            VacancyId = vacancyId
        });
    }
}