using IdentityServerLogic.Users.Interfaces;
using MassTransit;
using Services.Contracts;
using Services.Contracts.Vacancy;

namespace IdentityServerLogic;

public class UserResponseCounterConsumer: IConsumer<IIncrementUserRequest>,
    IConsumer<IDecrementUserRequest>
{
    private readonly IUserLogicManager _userLogicManager;
    
    public UserResponseCounterConsumer(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }
    
    public Task Consume(ConsumeContext<IIncrementUserRequest> context)
    {
        _userLogicManager.IncrementUserResponseAsync(context.Message.Id);
        
        return context.RespondAsync<IIncrementUserResponse>(new { Id = context.Message.Id });
    }

    public Task Consume(ConsumeContext<IDecrementUserRequest> context)
    {
        _userLogicManager.DicrementUserRespondeAsync(context.Message.Id);
        
        return context.RespondAsync<IDicrementVacancyResponse>(new { Id = context.Message.Id });
    }
}