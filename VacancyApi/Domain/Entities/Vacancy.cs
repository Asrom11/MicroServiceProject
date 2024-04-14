
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using ExampleCore.Dal.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Domain.Entities;

public record Vacancy: BaseEntity<Guid>
{
    public required Guid EmployerId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init;}
    
    public required VacancyStatus VacancyStatus { get; init; }
    
    public int ApplicationsCount { get; set; }
    
    [NotMapped]
    public CreatedVacancyUserInfo UserInfo { get; set; }
    
    [JsonIgnore]
    public VacancyFeedback[] VacancyFeedback { get; set; }
    public async Task<Guid> SaveAsycn(IStandartStore<Vacancy> storeVacancy, ICheckUser chekUser)
    {
        await chekUser.CheckEntityAsync(EmployerId);

        var res = await storeVacancy.CreateAsync(this);
        return res;
    }

    public async Task UpdateAsync(IStandartStore<Vacancy> standartStore, IStoreVacancy storeVacancy)
    {
        await storeVacancy.IsUserVacancyOwner(EmployerId, Id);
        await standartStore.UpdateAsync(this);
    }
}

public enum VacancyStatus
{
    Open,
    Closed
}