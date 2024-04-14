using Domain.Interfaces;
using ExampleCore.Dal.Base;

namespace Domain.Entities;

public record VacancyApplication: BaseEntity<Guid>
{
    public Guid VacancyId { get; set; }
    public Guid ApplicantId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    
    public async Task<Guid> SaveAsycn(IStandartStore<VacancyApplication> standartStore,IStoreApplication storeApplication, ICheckUser chekUser, ICheckVacancy checkVacancy)
    {
        // await chekUser.CheckEntityAsync(ApplicantId);
        //await checkVacancy.CheckEntityAsync(VacancyId);

        var res = await standartStore.CreateAsync(this);
        return res;
    }
    
    public async Task UpdateAsync(IStandartStore<VacancyApplication> standartStore, ICheckUser chekUser, ICheckVacancy checkVacancy, Guid employerId)
    {
        await CheckUserAndApplicationBelongsAsync(chekUser, employerId);

        await standartStore.UpdateAsync(this);
    }

    private async Task CheckUserAndApplicationBelongsAsync(ICheckEntity chekUser, Guid employerId)
    {
        
        
        await chekUser.CheckEntityAsync(ApplicantId);
    }
}

public enum ApplicationStatus
{
    Received,
    InReview,
    Accepted,
    Rejected
}