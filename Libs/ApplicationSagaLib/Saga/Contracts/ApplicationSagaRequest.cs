namespace Services.Contracts;

public class ApplicationSagaRequest
{
    public Guid Id { get; set; }
    
    public Guid VacancyId { get; set; }
    
    public Guid UserId { get; set; }
}