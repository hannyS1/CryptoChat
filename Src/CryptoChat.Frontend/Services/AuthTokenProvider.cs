using Blazored.LocalStorage;

namespace CryptoChat.Frontend.Services;

public class AuthTokenProvider
{
    private const string LocalStorageTokenKey = "AuthToken";
    private const string LocalStorageTokenExpiredKey = "AuthTokenExpired";
    
    private readonly ILocalStorageService _localStorageService;
    
    public AuthTokenProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<string> GetToken()
    {
        return await _localStorageService.GetItemAsStringAsync(LocalStorageTokenKey);
    }

    public async Task<bool> IsTokenExpired()
    {
        if (string.IsNullOrWhiteSpace(await _localStorageService.GetItemAsStringAsync(LocalStorageTokenExpiredKey)) ||
            string.IsNullOrWhiteSpace(await _localStorageService.GetItemAsStringAsync(LocalStorageTokenKey)))
        {
            return true;
        }
        return DateTime.Now >
               DateTime.Parse(await _localStorageService.GetItemAsStringAsync(LocalStorageTokenExpiredKey));
    }

    public async Task SetToken(string value)
    {
        await _localStorageService.SetItemAsStringAsync(LocalStorageTokenKey, value);
        await _localStorageService.SetItemAsStringAsync(
            LocalStorageTokenExpiredKey,
            DateTime.Now.AddDays(1).ToString());
    }
}