using System.Text;
using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.Entities;
using CryptoChat.Frontend.Common;
using Newtonsoft.Json;

namespace CryptoChat.Frontend.Services;

public class ChatService
{
    private readonly HttpClient _httpClient;
    private readonly AuthTokenProvider _authTokenProvider;
    
    public ChatService(HttpClient httpClient, AuthTokenProvider authTokenProvider)
    {
        _httpClient = httpClient;
        _authTokenProvider = authTokenProvider;
    }

    public async Task<List<RoomViewDto>> GetMyRooms()
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"{ChatControllerWebRoutes.BasePath}/{ChatControllerWebRoutes.GetMyRooms}"),
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
            return JsonConvert.DeserializeObject<List<RoomViewDto>>(content);
        }
    }
    
    public async Task<List<AnnotatedMessageDto>> GetRoomMessages(int roomId)
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"{ChatControllerWebRoutes.BasePath}/rooms/{roomId}/messages"),
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
            return JsonConvert.DeserializeObject<List<AnnotatedMessageDto>>(content);
        }
    }
    
    public async Task<MessageDto> SendMessage(int roomId, SendMessageRequestDto dto)
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();
        var content = JsonConvert.SerializeObject(dto);
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"{ChatControllerWebRoutes.BasePath}/rooms/{roomId}/send-message"),
            Headers =
            {
                { "Authorization", $"Bearer {userToken}" }
            },
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
        
        using (var response = await _httpClient.SendAsync(request))
        {
            if (!response.IsSuccessStatusCode)
                throw new ApiException(await response.Content.ReadAsStringAsync(), (int)response.StatusCode);
            
            content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MessageDto>(content);
        }
    }
}