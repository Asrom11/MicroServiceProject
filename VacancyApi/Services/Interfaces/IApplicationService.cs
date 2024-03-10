using Domain.Entities;

namespace Services.Interfaces;

public interface IApplicationService
{
    Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId);
    Task<VacancyApplication> GetApplicationByIdAsync(Guid id);
    Task<Guid> CreateApplicationAsync(VacancyApplication application);
    Task UpdateApplicationAsync(VacancyApplication application, Guid EmployerId);
    Task DeleteApplicationAsync(Guid applicationId, Guid userId);
}