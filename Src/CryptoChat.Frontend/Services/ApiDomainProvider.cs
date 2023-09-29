namespace CryptoChat.Frontend.Services;

public class ApiDomainProvider
{
    public ApiDomainProvider(IConfiguration configuration)
    {
        Domain = configuration["apiDomain"];
    }
    
    public string Domain { get; private set; }
}