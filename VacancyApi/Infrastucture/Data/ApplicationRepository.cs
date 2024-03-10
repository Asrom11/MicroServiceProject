using Domain.Entities;
using Domain.Interfaces;
using Medo;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Data;

public class ApplicationRepository: IStoreApplication
{

    private readonly ApplicationDbContext _applicationDbContext;
    public ApplicationRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<VacancyApplication> IsUserApplicationAsync(Guid applicationId, Guid userId)
    {
        var application = await _applicationDbContext.VacancyApplications
            .FirstOrDefaultAsync(a => a.Id == applicationId && a.ApplicantId == userId);
    
        if (application == null)
        {
            throw new InvalidOperationException("Application does not exist or user is not the owner.");
        }
    
        return application;
    }

    public async Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId)
    {
        var applications = await _applicationDbContext.VacancyApplications
            .Where(a => a.VacancyId == vacancyId)
            .ToListAsync();
    
        return applications;
    }
}