﻿using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChat.Host.Controllers;

[Route(UserControllerWebRoutes.BasePath)]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }

    [Authorize]
    [HttpGet(UserControllerWebRoutes.GetCurrentUser)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        if (user == null)
            return Unauthorized(new { message = "invalid token" });
        
        return Ok(user);
    }

    [Authorize]
    [HttpGet(UserControllerWebRoutes.GetAllExceptMe)]
    public async Task<ActionResult<UserDto>> GetAllExceptMe()
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        return Ok(await _userService.GetAllExceptOne(user.Id));
    }
    
}