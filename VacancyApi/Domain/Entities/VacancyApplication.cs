using Domain.Interfaces;
using ExampleCore.Dal.Base;
using Services.Interfaces;

namespace Domain.Entities;

public record VacancyApplication: BaseEntityDal<Guid>
{
    public Guid VacancyId { get; set; }
    public Guid ApplicantId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    
    public async Task<Guid> SaveAsycn(IStandartStore<VacancyApplication> standartStore,IStoreApplication storeApplication, IChekUser chekUser, IStoreVacancy storeVacancy)
    {
        await chekUser.CheckUserExistAsync(ApplicantId);
        await storeVacancy.CheckExcistVacancy(VacancyId);

        var res = await standartStore.CreateAsync(this);
        return res;
    }
    
    public async Task UpdateAsync(IStandartStore<VacancyApplication> standartStore, IChekUser chekUser, IStoreVacancy storeVacancy, Guid employerId)
    {
        await CheckUserAndApplicationBelongsAsync(storeVacancy, chekUser, employerId);

        await standartStore.UpdateAsync(this);
    }

    private async Task CheckUserAndApplicationBelongsAsync(IStoreVacancy storeVacancy, IChekUser chekUser, Guid employerId)
    {
        await chekUser.CheckUserExistAsync(ApplicantId);
        
        await storeVacancy.IsUserVacancyOwner(VacancyId, employerId);
    }
}

public enum ApplicationStatus
{
    Received,
    InReview,
    Accepted,
    Rejected
}