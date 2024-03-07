using Domain.Entities;

namespace Services.Interfaces;

public interface IVacancyService
{
    Task<Guid> CreateVacancyAsync(Vacancy post);

    Task UpdateVacancyAsync(Vacancy post);

    Task<IEnumerable<Vacancy>> GetPostListasync();
}