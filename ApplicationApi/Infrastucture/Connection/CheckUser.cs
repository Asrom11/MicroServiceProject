using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace Infrastucture.Connection;

public class CheckUser: ICheckUser
{
    private readonly IProfileConnectionServcie _profileConnectionServcie;

    public CheckUser(IProfileConnectionServcie profileConnectionServcie)
    {
        _profileConnectionServcie = profileConnectionServcie;
    }

    public async Task CheckEntityAsync(Guid entityId)
    {
        await _profileConnectionServcie.CheckUserExistAsync(new CheckUserExistProfileApiRequest()
        {
           UserId = entityId
        });
    }
}