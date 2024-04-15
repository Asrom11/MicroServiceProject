using Domain.Entities;
using Domain.Interfaces;
using Medo;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Data;

public class VacancyRepository: IStoreVacancy
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public VacancyRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task CheckExcistVacancy(Guid vacancyId)
    {
        if (!await _applicationDbContext.Vacancies.AnyAsync(vacancy => vacancy.Id == vacancyId))
        {
            throw new Exception("Vacancy doesn't exist");
        }
    }

    public async Task IsUserVacancyOwner(Guid userId, Guid vacancyId)
    {
        var isOwner = await _applicationDbContext.Vacancies
            .AnyAsync(v => v.Id == vacancyId && v.EmployerId == userId);
        
        if (!isOwner)
        {
            throw new InvalidOperationException("The user is not the owner of the vacancy.");
        }
    }

    public async Task<Guid> GetVacancyIdAsync(Guid id)
    {
        var vacancyId = await _applicationDbContext.Vacancies
            .Where(u => u.Id == id)
            .Select(u => u.Id)
            .FirstOrDefaultAsync();
        return vacancyId;
    }

    public async Task IncrementApplicationCountAsync(Guid id)
    {
        var vacancy = await GetVacancyAsync(id);

        vacancy.ApplicationsCount++;

        await _applicationDbContext.SaveChangesAsync();
    }

    private async Task<Vacancy> GetVacancyAsync(Guid id)
    {
        var vacancy = await _applicationDbContext.Vacancies.
            FirstOrDefaultAsync(v => v.Id == id);

        if (vacancy is null)
        {
            throw new Exception("Vacancy not found");
        }

        return vacancy;
    }

    public async Task DicrementApplicationCountAsync(Guid id)
    {
        var vacancy = await GetVacancyAsync(id);

        vacancy.ApplicationsCount--;

        await _applicationDbContext.SaveChangesAsync();
    }
}