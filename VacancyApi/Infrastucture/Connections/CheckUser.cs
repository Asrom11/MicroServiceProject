using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;

namespace Infrastucture.Connections;

public class CheckUser: ICheckUser
{
    private readonly IProfileConnectionServcie  _profileConnectionServcie;

    public CheckUser(IProfileConnectionServcie profileConnectionServcie)
    {
        _profileConnectionServcie = profileConnectionServcie;
    }
    
    public async Task CheckEntityAsync(Guid userId)
    {
        await _profileConnectionServcie.CheckUserExistAsync(new CheckUserExistProfileApiRequest
        {
            UserId = userId
        });
    }
}