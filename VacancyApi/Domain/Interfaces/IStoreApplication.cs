using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreApplication
{
    public Task<VacancyApplication> IsUserApplicationAsync(Guid applicationId, Guid userId);

    public Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId);
}