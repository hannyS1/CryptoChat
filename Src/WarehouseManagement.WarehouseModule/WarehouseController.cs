using CryptoChat.Api.Contracts.Data;
using CryptoChat.Database;
using CryptoChat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Common.Extensions;

namespace WarehouseManagement.WarehouseModule;

[Authorize]
[Route("api/warehouse")]
[ApiController]
public class WarehouseController : Controller
{
    private readonly ApplicationContext _dbContext;
    private readonly HttpContext _httpContext;
    
    public WarehouseController(ApplicationContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContext = httpContextAccessor.HttpContext;
    }
    
    [HttpGet("items")]
    public async Task<ActionResult<List<WarehouseItem>>> GetItems()
    {
        var userId = _httpContext.GetUserId();
        
        var user = await _dbContext.Users.Include(u => u.AllowedCategory).FirstAsync(u => u.Id == userId);
        var items = await _dbContext.WarehouseItems
            .Include(i => i.Category)
            .Where(i => i.CategoryId == user.AllowedCategoryId)
            .ToListAsync();
        
        return Ok(items);
    }

    [HttpPost("items")]
    public async Task<ActionResult<WarehouseItem>> CreateItem([FromBody] WarehouseItemCreateRequestDto dto)
    {
        var itemEntry = await _dbContext.WarehouseItems.AddAsync(new WarehouseItem
        {
            Name = dto.Name,
            Count = dto.Count,
            CategoryId = dto.Count,
        });
        
        await _dbContext.SaveChangesAsync();
        return Ok(itemEntry.Entity);
    }
    
    [HttpPatch("items/{id:int}")]
    public async Task<ActionResult<WarehouseItem>> UpdateItem([FromRoute] int id, [FromBody] WarehouseItemUpdateRequestDto dto)
    {
        var item = await _dbContext.WarehouseItems.FindAsync(id);
        if (item == null)
            return BadRequest("Неверный айди позиции");
        
        item.Count = dto.Count;
        await _dbContext.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("items/{id:int}")]
    public async Task<ActionResult> DeleteItem([FromRoute] int id)
    {
        var item = new WarehouseItem { Id = id };
        _dbContext.WarehouseItems.Attach(item);
        _dbContext.WarehouseItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<WarehouseItemCategory>>> GetCategories()
    {
        return await _dbContext.WarehouseItemCategories.ToListAsync();
    }
}