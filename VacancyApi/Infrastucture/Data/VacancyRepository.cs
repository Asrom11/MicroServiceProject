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
}