using Domain.Entities;

namespace Api.Controllers.Vacancy.Requests;

public record UpdateVacancyRequest: CreateVacancyRequest
{
    public required Guid VacancyId { get; set; }
    public required VacancyStatus VacancyStatus { get; set; }
}