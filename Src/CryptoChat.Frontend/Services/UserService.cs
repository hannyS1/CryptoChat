using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.Frontend.Common;
using Newtonsoft.Json;

namespace CryptoChat.Frontend.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly AuthTokenProvider _authTokenProvider;
    
    public UserService(HttpClient httpClient, AuthTokenProvider authTokenProvider)
    {
        _httpClient = httpClient;
        _authTokenProvider = authTokenProvider;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"{UserControllerWebRoutes.BasePath}/{UserControllerWebRoutes.GetAllExceptMe}"),
            Headers =
            {
                { "Authorization", $"Bearer {userToken}" }
            }
        };
        
        using (var response = await _httpClient.SendAsync(request))
        {
            if (!response.IsSuccessStatusCode)
                throw new ApiException(await response.Content.ReadAsStringAsync(), (int)response.StatusCode);
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDto>>(content);
        }

    }
}