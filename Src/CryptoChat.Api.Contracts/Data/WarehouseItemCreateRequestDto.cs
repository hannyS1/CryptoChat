namespace CryptoChat.Api.Contracts.Data;

public class WarehouseItemCreateRequestDto
{
    public string Name { get; set; }
    public int Count { get; set; }
    public int CategoryId { get; set; }
}