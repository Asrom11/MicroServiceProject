using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Data;

public class VacancyFeedbackRepository: IStoreFeedback
{
    private readonly ApplicationDbContext _applicationDbContext;
    public VacancyFeedbackRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<List<VacancyFeedback>> GetByVacancyIdAsync(Guid vacancyId)
    {
        var feedbacks = await _applicationDbContext.VacancyFeedbacks
            .Where(f => f.VacancyId == vacancyId).ToListAsync();

        return feedbacks;
    }

    public async Task<VacancyFeedback> IsUserFeedback(Guid feedbackId, Guid userId)
    {
        var feedback = await _applicationDbContext.VacancyFeedbacks
            .FirstOrDefaultAsync(f => f.Id == feedbackId && f.ApplicantId == userId);
    
        if (feedback == null)
        {
            throw new InvalidOperationException("User is not the author of the feedback or feedback does not exist.");
        }
    
        return feedback;
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var entity = await _applicationDbContext.VacancyFeedbacks
            .FirstOrDefaultAsync(e => e.Id == id && e.ApplicantId == userId);
    
        if (entity == null)
        {
            throw new InvalidOperationException("Entity does not exist or user is not the owner.");
        }
        
        _applicationDbContext.VacancyFeedbacks.Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
    }
}