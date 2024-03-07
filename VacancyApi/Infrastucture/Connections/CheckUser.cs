using Services.Interfaces;

namespace Infrastucture.Connections;

public class CheckUser: IChekUser
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CheckUser(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task CheckUserExistAsync(Guid userId)
    {
        var client = _httpClientFactory.CreateClient();
        var res = await client.GetAsync("temp");
        
        if (res is null)
        {
            throw new Exception("User not found");
        }
    }
}

public interface IHttpClientFactory
{
    public HttpClient CreateClient();
}