﻿using CryptoChat.Api.Contracts.Data;
using CryptoChat.Api.Contracts.WebRoutes;
using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.Common.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChat.Host.Controllers;

[Route(JwtAuthenticateControllerWebRoutes.BasePath)]
public class JwtAuthenticateController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public JwtAuthenticateController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }
    
    [HttpPost(JwtAuthenticateControllerWebRoutes.LoginPasswordAuth)]
    public async Task<ActionResult<JwtAuthResponseDto>> LoginPasswordAuth([FromBody] LoginPasswordAuthRequestDto dto)
    {
        var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
        if (user == null)
            return BadRequest(new { Message = "Invalid username or password" });
        
        var token = _tokenService.CreateToken(user.Id);
        return Ok(new JwtAuthResponseDto { AccessToken = token });
    }

    [HttpPost(JwtAuthenticateControllerWebRoutes.RegisterUser)]
    public async Task<ActionResult<JwtAuthResponseDto>> RegisterUser([FromBody] UserRegisterRequestDto dto)
    {
        var user = await _userService.Create(dto.Name, dto.Password);
        var token = _tokenService.CreateToken(user.Id);
        return new JwtAuthResponseDto { AccessToken = token };
    }
}