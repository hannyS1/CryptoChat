using System.Net.Http.Json;
using Blazored.LocalStorage;
using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.Frontend.Common;
using Newtonsoft.Json;

namespace CryptoChat.Frontend.Services;

public class AuthenticateService
{
    private readonly HttpClient _httpClient;
    private readonly AuthTokenProvider _authTokenProvider;
    private readonly ILocalStorageService _localStorageService;
    
    public AuthenticateService(
        HttpClient httpClient, 
        AuthTokenProvider authTokenProvider, 
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authTokenProvider = authTokenProvider;
        _localStorageService = localStorageService;
    }
    
    public async Task<JwtAuthResponseDto> Login(string username, string password)
    {

        var response = await _httpClient.PostAsJsonAsync(
            $"/{JwtAuthenticateControllerWebRoutes.BasePath}" +
            $"/{JwtAuthenticateControllerWebRoutes.LoginPasswordAuth}",
            new LoginPasswordAuthRequestDto { Username = username, Password = password });

        if (!response.IsSuccessStatusCode)
            throw new ApiException(await response.Content.ReadAsStringAsync(), (int)response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<JwtAuthResponseDto>(content);
    }

    public async Task<bool> IsAuthenticated()
    {
        return !await _authTokenProvider.IsTokenExpired();
    }

    public async Task<UserDto> GetMyselfInfo()
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}{UserControllerWebRoutes.BasePath}/{UserControllerWebRoutes.GetCurrentUser}"),
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
            return JsonConvert.DeserializeObject<UserDto>(content);
        }
    }

    public async Task<UserDto> GetMyselfInfoFromStorage()
    {
        int userId;
        var userIdString = await _localStorageService.GetItemAsStringAsync("userId");
        var username = await _localStorageService.GetItemAsStringAsync("username");

        if (userIdString == null || username == null)
        {
            var user = await GetMyselfInfo();
            userId = user.Id;
            username = user.Name;
        }
        else
        {
            userId = Int32.Parse(userIdString);
        }

        return new UserDto
        {
            Id = userId,
            Name = username,
        };
    }
}