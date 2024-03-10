using Domain.Entities;

namespace Domain.Interfaces;

public interface IStoreVacancy
{
    Task CheckExcistVacancy(Guid vacancyId);
    
    Task IsUserVacancyOwner(Guid userId, Guid vacancyId);
}