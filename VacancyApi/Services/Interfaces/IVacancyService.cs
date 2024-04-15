using Domain.Entities;

namespace Services.Interfaces;

public interface IVacancyService
{
    Task<Guid> CreateVacancyAsync(Vacancy post);

    Task UpdateVacancyAsync(Vacancy post);

    Task<List<Vacancy>> GetVacancyListAsync();
    
    Task<Guid> CheckVacancyExistAsync(Guid userId);

    Task IncrementVacancyAsync(Guid vacancyId);

    Task DicrementVacncyAsync(Guid vacancyId);
}