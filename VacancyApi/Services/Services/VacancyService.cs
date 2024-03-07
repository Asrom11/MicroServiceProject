using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class VacancyService: IVacancyService
{
    private readonly IChekUser _chekUser;
    private readonly IStoreVacancy _storeVacancy;
    private readonly IStandartStore<Vacancy> _standartStore;
    
    public VacancyService(IStandartStore<Vacancy> standartStore, IChekUser chekUser, IStoreVacancy storeVacancy)
    {
        _standartStore = standartStore;
        _chekUser = chekUser;
        _storeVacancy = storeVacancy;
   }
    public async Task<Guid> CreateVacancyAsync(Vacancy post)
    {
        var res = await post.SaveAsycn(_standartStore, _chekUser);
        return res;
    }

    public async Task UpdateVacancyAsync(Vacancy post)
    { 
        await post.UpdateAsync(_standartStore,_storeVacancy,_chekUser);
    }

    public Task<IEnumerable<Vacancy>> GetPostListasync()
    {
        throw new NotImplementedException();
    }
}