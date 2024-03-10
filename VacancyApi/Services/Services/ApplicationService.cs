using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class ApplicationService: IApplicationService
{
    private readonly IStoreApplication _storeApplication;
    private readonly IChekUser _chekUser;
    private readonly IStoreVacancy _storeVacancy;
    private readonly IStandartStore<VacancyApplication> _standartStore;
    
    public ApplicationService(IStoreApplication storeApplication, IChekUser chekUser, IStoreVacancy storeVacancy, IStandartStore<VacancyApplication> standartStore)
    {
        _standartStore = standartStore;
        _storeApplication = storeApplication;
        _chekUser = chekUser;
        _storeVacancy = storeVacancy;
    }
    public async Task<IEnumerable<VacancyApplication>> GetApplicationsByVacancyIdAsync(Guid vacancyId)
    {
        var res = await _storeApplication.GetApplicationsByVacancyIdAsync(vacancyId);
        return res;
    }

    public async Task<VacancyApplication> GetApplicationByIdAsync(Guid id)
    {
        var res = await _standartStore.GetByIdAsync(id);
        return res;
    }

    public async Task<Guid> CreateApplicationAsync(VacancyApplication application)
    {
        var res = await application.SaveAsycn(_standartStore,_storeApplication, _chekUser, _storeVacancy);
        return res;
    }

    public async Task UpdateApplicationAsync(VacancyApplication application, Guid employerId)
    {
        await application.UpdateAsync(_standartStore, _chekUser,_storeVacancy, employerId);
    }

    public async Task DeleteApplicationAsync(Guid applicationId, Guid userId)
    {
       var entity = await _storeApplication.IsUserApplicationAsync(applicationId, userId);
       await _standartStore.DeleteAsync(entity);
    }
}