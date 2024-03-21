using IdentityServerLogic.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;

namespace IdentityServerLogic;

public class UserExistConsumer : ConsumerBase<CheckUserExistProfileApiRequest,CheckUserExistProfileApiResponse>
{
    
    public UserExistConsumer(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }
    
    protected override async Task<CheckUserExistProfileApiResponse> ProcessMessage(CheckUserExistProfileApiRequest message)
    {
        using (var scope = ScopeFactory.CreateScope())
        {
            
            var scopedService = scope.ServiceProvider.GetRequiredService<IUserLogicManager>();
            var id = await scopedService.CheckUserExist(message.UserId);

            var res = new CheckUserExistProfileApiResponse()
            {
                UserId = id
            };
            
            return res;
        }
    }

    protected override string SerializeResponse(CheckUserExistProfileApiResponse response)
    {
        return JsonConvert.SerializeObject(response);
    }
    

    protected override string SerializeErrorResponse()
    {
        return JsonConvert.SerializeObject(new CheckUserExistProfileApiResponse()
        {
            UserId = Guid.Empty
        });
    }
    
}