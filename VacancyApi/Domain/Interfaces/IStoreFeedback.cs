using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreFeedback
{
    Task<List<VacancyFeedback>> GetByVacancyIdAsync(Guid vacancyId);
    Task<VacancyFeedback> IsUserFeedback(Guid feedbackId, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
}