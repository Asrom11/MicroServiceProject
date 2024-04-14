using MassTransit;
using MassTransit.Contracts;
using MassTransit.Mediator;
using Services.Contracts;
using Services.Contracts.Application;
using Services.Contracts.Vacancy;

namespace Services;

public class ApplicationStateMachine: 
    MassTransitStateMachine<ApplicationCreateState>
{
    public Request<ApplicationCreateState, IVacancyIncrementRequest, IVacancyIncrementResponse> IncrementVacancy { get; set; }
    public Request<ApplicationCreateState, IIncrementUserRequest, IIncrementUserResponse> IncrementUser { get; set; }
    public Request<ApplicationCreateState, CreateApplicationRequest, CreateApplicationResponse> CreateApplication { get; set; }
    
    public Event<ApplicationSagaRequest> CreateApplicationEvent { get; set; }
    public State Failed { get; set; }
    
    public ApplicationStateMachine()
    {
        InstanceState(x => x.CurrentState);
        
        Event(() => CreateApplicationEvent, e =>
        {
            e.CorrelateById(y => y.Message.Id);
            e.InsertOnInitial = true;
        });
        
        
        
        Request(
            () => IncrementVacancy
            );
        Request(
         () => IncrementUser
         );
        Request(() => CreateApplication
        );
        
        Initially(
            When(CreateApplicationEvent)
            .Then(context =>
            {
                if (!context.TryGetPayload(out SagaConsumeContext<ApplicationCreateState, ApplicationSagaRequest> payload))
                    throw new Exception("Unable to retrieve required payload for callback data.");

                var saga = context.Saga;
                saga.RequestId = payload.RequestId;
                saga.ResponseAddress = payload.ResponseAddress;
                saga.UserId = payload.Message.UserId;
                saga.VacancyId = payload.Message.VacancyId;
                saga.ApplicationId = payload.Message.Id;
            })
            .Request(IncrementVacancy, context => context.Init<IVacancyIncrementRequest>(new 
            {
                Id = context.Saga.VacancyId
            }))
            .TransitionTo(IncrementVacancy.Pending));
        
            During(IncrementVacancy.Pending,
                When(IncrementVacancy.Completed)
            .Request(IncrementUser, x => 
                x.Init<IIncrementUserRequest>(new { UserId = x.Saga.UserId }))
            .TransitionTo(IncrementUser.Pending),
            
            When(IncrementVacancy.Faulted)
              .ThenAsync(async context =>
              { 
               await RespondFromSaga(context, "Faulted On Increment Vacancy " + string.Join("; ", context.Message.Exceptions.Select(x => x.Message)));
              })    
            .TransitionTo(Failed),
                When(IncrementVacancy.TimeoutExpired)
                    .ThenAsync(async context =>
                    {
                        await RespondFromSaga(context, "Timeout Expired On Increment Vacancy");
                    })    
                    .TransitionTo(Failed)
            );

            During(IncrementUser.Pending,
                When(IncrementUser.Completed)
                    .Request(CreateApplication, x => x.Init<CreateApplicationRequest>(new CreateApplicationRequest()
                    {
                        Id = x.Saga.ApplicationId,
                        UserId = x.Saga.UserId,
                        VacancyId = x.Saga.VacancyId
                    }))
                    .TransitionTo(CreateApplication.Pending),
                When(IncrementUser.Faulted)
                    .ThenAsync(async context =>
                    {
                        await RespondFromSaga(context, "Timeout Expired On Increment User");
                    })
                    .TransitionTo(Failed)
            );

            During(CreateApplication.Pending,
                When(CreateApplication.Completed)
                    .ThenAsync(async context =>
                    {
                        await RespondFromSaga(context, null);
                    })
                    .Finalize()
                , When(CreateApplication.Faulted)
                    .ThenAsync(async context => await RespondFromSaga(context, "Faulted On Create Application " + string.Join("; ", context.Message.Exceptions.Select(x => x.Message))))
                    .TransitionTo(Failed),
                When(CreateApplication.TimeoutExpired)
                    .ThenAsync(async context => await RespondFromSaga(context, "Timeout Expired On CreateApplication"))
                    .TransitionTo(Failed)
            );


            SetCompletedWhenFinalized();
    }

    private static async Task RespondFromSaga<T>(BehaviorContext<ApplicationCreateState, T> context, string error) where T : class
    {
        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
        await endpoint.Send(new CreateApplicationResponse
        {
            Id = context.Saga.CorrelationId,
            ErrorMessage = error
        }, r => r.RequestId = context.Saga.RequestId);
    }
}