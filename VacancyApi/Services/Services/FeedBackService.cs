using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class FeedBackService: IFeedbackService
{

    private readonly IStoreFeedback _storeFeedback;
    private readonly IChekUser _chekUser;
    private readonly IStoreVacancy _storeVacancy;
    private readonly IStandartStore<VacancyFeedback> _standartStore;
    
    public FeedBackService(IStandartStore<VacancyFeedback> standartStore, IStoreFeedback storeFeedback, IChekUser chekUser, IStoreVacancy storeVacancy)
    {
        _standartStore = standartStore;
        _storeFeedback = storeFeedback;
        _chekUser = chekUser;
        _storeVacancy = storeVacancy;
    }
    public async Task<List<VacancyFeedback>> GetFeedbackByVacancyId(Guid vacancyId)
    {
        var res = await _storeFeedback.GetByVacancyIdAsync(vacancyId);
        // and get name
        return res;
    }

    public async Task<Guid> CreateFeedback(VacancyFeedback feedback)
    {
        var res = await feedback.SaveAsync(_standartStore, _chekUser, _storeVacancy);
        return res;
    }

    public async Task UpdateFeedback(Guid id, VacancyFeedback feedback)
    {
        await feedback.UpdateAsync(_standartStore,_storeFeedback,_storeVacancy, id);
    }

    public async Task DeleteFeedback(Guid id, Guid userId)
    {
       await _storeFeedback.DeleteAsync(id, userId);
    }
}