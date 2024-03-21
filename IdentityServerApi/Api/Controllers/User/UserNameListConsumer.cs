using IdentityServerLogic.Users.Interfaces;
using Newtonsoft.Json;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;

namespace IdentityServerLogic;

public class UserNameListConsumer: ConsumerBase<UserNameListProfileApiRequest, List<UserNameListProfileApiResponse>>
{
    public UserNameListConsumer(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    protected override async Task<List<UserNameListProfileApiResponse>> ProcessMessage(UserNameListProfileApiRequest message)
    {
        using (var scope = ScopeFactory.CreateScope())
        {
            var scopedService = scope.ServiceProvider.GetRequiredService<IUserLogicManager>();
            var userList = await scopedService.GetUserNameListAsync(message.UserIdList);
            var res = userList.Select(user => new UserNameListProfileApiResponse
            {
                UserList = new UserList
                {
                    Name = user.Name,
                    UserId = user.Id 
                }
            }).ToList();
            
            return res;
        }
    }

    protected override string SerializeResponse(List<UserNameListProfileApiResponse> response)
    {
        return JsonConvert.SerializeObject(response);
    }
    
    protected override string SerializeErrorResponse()
    {
        return string.Empty;
    }
}