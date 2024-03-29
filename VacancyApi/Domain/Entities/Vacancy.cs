﻿
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using ExampleCore.Dal.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Interfaces;

namespace Domain.Entities;

public record Vacancy: BaseEntity<Guid>
{
    public required Guid EmployerId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init;}
    
    public required VacancyStatus VacancyStatus { get; init; }
    
    [NotMapped]
    public CreatedVacancyUserInfo UserInfo { get; set; }
    
    [JsonIgnore]
    public VacancyApplication[] VacancyApplication { get; set; }
    
    [JsonIgnore]
    public VacancyFeedback[] VacancyFeedback { get; set; }
    public async Task<Guid> SaveAsycn(IStandartStore<Vacancy> storeVacancy, IChekUser chekUser)
    {
        await chekUser.CheckUserExistAsync(EmployerId);

        var res = await storeVacancy.CreateAsync(this);
        return res;
    }

    public async Task UpdateAsync(IStandartStore<Vacancy> standartStore, IStoreVacancy storeVacancy, IChekUser chekUser)
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