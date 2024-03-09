using Domain.Entities;

namespace Services.Interfaces;

public interface IFeedbackService
{
    Task<List<VacancyFeedback>> GetFeedbackByVacancyId(Guid vacancyId);
    Task<Guid> CreateFeedback(VacancyFeedback feedback);
    Task UpdateFeedback(Guid id, VacancyFeedback feedback);
    Task DeleteFeedback(Guid id, Guid userId);
}