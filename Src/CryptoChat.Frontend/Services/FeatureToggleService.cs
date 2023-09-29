using CryptoChat.Entities;
using Newtonsoft.Json;

namespace CryptoChat.Frontend.Services;

public class FeatureToggleService
{
    private readonly HttpClient _httpClient;
    
    public FeatureToggleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetEnabledFeatures()
    {
        var response = await _httpClient.GetAsync(new Uri($"{_httpClient.BaseAddress.AbsoluteUri}api/features"));
        var content = await response.Content.ReadAsStringAsync();

        List<FeatureToggle> features;
        try
        {
            features = JsonConvert.DeserializeObject<List<FeatureToggle>>(content);
        }
        catch
        {
            return new List<string>();
        }
        
        if (features == null || features.Count == 0)
            return new List<string>();
        
        return features.Where(f => f.Enabled).Select(f => f.Key).ToList();
    }
}