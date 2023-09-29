using System.Text;
using CryptoChat.Api.Contracts.Data;
using CryptoChat.Entities;
using CryptoChat.Frontend.Common;
using Newtonsoft.Json;

namespace CryptoChat.Frontend.Services;

public class WarehouseService
{
    private readonly HttpClient _httpClient;
    private readonly AuthTokenProvider _authTokenProvider;
    
    public WarehouseService(HttpClient httpClient, AuthTokenProvider authTokenProvider)
    {
        _httpClient = httpClient;
        _authTokenProvider = authTokenProvider;
    }

    public async Task<List<WarehouseItem>> GetItems()
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"api/warehouse/items"),
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
            return JsonConvert.DeserializeObject<List<WarehouseItem>>(content);
        }
    }
    
    public async Task<WarehouseItem> UpdateItemCount(int itemId, int count)
    {
        if (await _authTokenProvider.IsTokenExpired())
            throw new ClientApplicationException("Вы не авторизованы");
        
        var userToken = await _authTokenProvider.GetToken();
        var content = JsonConvert.SerializeObject(new WarehouseItemUpdateRequestDto { Count = count });
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Patch,
            RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}" +
                                 $"api/warehouse/items/{itemId}"),
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
            return JsonConvert.DeserializeObject<WarehouseItem>(content);
        }
    }
}