using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;

namespace Infrastucture.Connections;

public class CheckUser: IChekUser
{
    private readonly IProfileConnectionServcie  _profileConnectionServcie;

    public CheckUser(IProfileConnectionServcie profileConnectionServcie)
    {
        _profileConnectionServcie = profileConnectionServcie;
    }
    
    public async Task CheckUserExistAsync(Guid userId)
    {
        await _profileConnectionServcie.CheckUserExistAsync(new CheckUserExistProfileApiRequest
        {
            UserId = userId
        });
    }
}