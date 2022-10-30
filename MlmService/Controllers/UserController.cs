﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MlmService.Extensions;
using MlmService.Services.Interface;

namespace MlmService.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("userinfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = await _userService.GetUserInfo(HttpContext.GetUserId());
        if (user == null)
            return NoContent();


        return Ok(user);
    }

}