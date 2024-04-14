using Domain.Interfaces;
using ExampleCore.Dal.Base;

namespace Domain.Entities;

public record VacancyFeedback: BaseEntity<Guid>
{
    public required Guid VacancyId { get; init; }
    public required Guid ApplicantId { get; init; }
    public required string Comment { get; set; }
    public required double Rating { get; set; }
    public  DateTime SubmittedDate { get; set; }

    public async Task<Guid> SaveAsync(IStandartStore<VacancyFeedback> standartStore, ICheckUser chekUser, IStoreVacancy storeVacancy)
    {
        await chekUser.CheckEntityAsync(ApplicantId);
        await storeVacancy.CheckExcistVacancy(VacancyId);
        
        var res =  await standartStore.CreateAsync(this);

        return res;
    }
    
    public async Task UpdateAsync(IStandartStore<VacancyFeedback> standartStore, IStoreFeedback storeFeedback, IStoreVacancy storeVacancy, Guid userID)
    {
        await storeVacancy.CheckExcistVacancy(VacancyId);
        var feedback = await storeFeedback.IsUserFeedback(Id, userID);
        SubmittedDate = feedback.SubmittedDate;
        
        await standartStore.UpdateAsync(this);
    }
}