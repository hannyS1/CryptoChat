using CryptoChat.Database;
using CryptoChat.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.FeatureToggleModule;

[Route("api/features")]
public class FeatureToggleController : Controller
{
    private readonly ApplicationContext _dbContext;
    
    public FeatureToggleController(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<FeatureToggle>>> GetAll()
    {
        return Ok(await _dbContext.FeatureToggles.ToListAsync());
    }
}