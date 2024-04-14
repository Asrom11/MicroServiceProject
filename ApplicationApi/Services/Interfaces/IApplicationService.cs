using Domain.Entities;
using Services.Contracts.Application;

namespace Services.Interfaces;

public interface IApplicationService
{
    Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId);
    Task<VacancyApplication> GetApplicationByIdAsync(Guid id);
    Task<Guid> CreateApplicationAsync(VacancyApplication application);
    Task<CreateApplicationResponse> CreateApplicationSagaAsync(VacancyApplication application);
    Task UpdateApplicationAsync(VacancyApplication application, Guid EmployerId);
    Task DeleteApplicationAsync(Guid applicationId, Guid userId);
}