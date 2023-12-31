﻿namespace CryptoChat.Api.Contracts.Data;

public class UserDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public WareHouseItemCategoryDto AllowedCategory { get; set; }
}