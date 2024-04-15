using MassTransit;

namespace Services;

public class ApplicationCreateState :
    SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string? CurrentState { get; set; }
    public Guid? RequestId { get; set; }
    public Uri? ResponseAddress { get; set; }
    
    public Guid UserId { get; set; }
    public Guid VacancyId { get; set; }
    
    public Guid ApplicationId { get; set; }
}
