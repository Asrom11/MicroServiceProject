using Domain.Entities;
using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class VacancyService: IVacancyService
{
    private readonly ICheckUser _chekUser;
    private readonly IStoreVacancy _storeVacancy;
    private readonly IStandartStore<Vacancy> _standartStore;
    private readonly IProfileConnectionServcie _profileConnectionServcie;
    
    public VacancyService(IStandartStore<Vacancy> standartStore, ICheckUser chekUser, IStoreVacancy storeVacancy, IProfileConnectionServcie profileConnectionServcie)
    {
        _profileConnectionServcie = profileConnectionServcie;
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
        await post.UpdateAsync(_standartStore,_storeVacancy);
    }

    public  async Task<List<Vacancy>> GetVacancyListAsync()
    {
        var vacancyList =  await _standartStore.GetAllAsync();
        var employerIdList = vacancyList.Select(value => value.EmployerId).ToArray();

        var employerNameList = await _profileConnectionServcie.GetUserNameListAsync(new UserNameListProfileApiRequest()
        {
            UserIdList = employerIdList
        });
        var employerNamesDict = employerNameList.ToDictionary(user => user.UserList.UserId, user => user.UserList.Name);
        
        foreach (var vacancy in vacancyList)
        {
            vacancy.UserInfo = employerNamesDict.TryGetValue(vacancy.EmployerId, out var employerName) 
                ? new CreatedVacancyUserInfo { Name = employerName } : new CreatedVacancyUserInfo { Name = "Lost" };
        }
        
        return vacancyList;
    }

    public async Task<Guid> CheckVacancyExistAsync(Guid vacancyId)
    {
        var res = await _storeVacancy.GetVacancyIdAsync(vacancyId);

        return res;
    }

    public async Task IncrementVacancyAsync(Guid vacancyId)
    {
        await _storeVacancy.IncrementApplicationCountAsync(vacancyId);
    }

    public async Task DicrementVacncyAsync(Guid vacancyId)
    {
        await _storeVacancy.DicrementApplicationCountAsync(vacancyId);
    }
}