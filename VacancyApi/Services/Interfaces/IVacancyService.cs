using Domain.Entities;

namespace Services.Interfaces;

public interface IVacancyService
{
    Task<Guid> CreateVacancyAsync(Vacancy post);

    Task UpdateVacancyAsync(Vacancy post);

    Task<List<Vacancy>> GetVacancyListAsync();
}