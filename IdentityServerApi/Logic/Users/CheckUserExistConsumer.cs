using IdentityServerLogic.Users.Interfaces;
using MassTransit;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace IdentityServerLogic.Users;

public class CheckUserExistConsumer : IConsumer<CheckUserExistProfileApiRequest>
{
    private readonly IUserLogicManager _userLogicManager;

    public CheckUserExistConsumer(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }
    
    public async Task Consume(ConsumeContext<CheckUserExistProfileApiRequest> context)
    {
        var result = await _userLogicManager.CheckUserExist(context.Message.UserId);
        
        var response = new CheckUserExistProfileApiResponse
        {
            UserId = result
        };
        
        await context.RespondAsync(response);
    }
}
