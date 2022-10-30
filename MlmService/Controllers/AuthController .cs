using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MlmService.Contracts;
using MlmService.Dto.auth;
using MlmService.Dto.Auth;
using MlmService.Dto.Authentication.Facebook;
using MlmService.Extensions;

namespace MlmService.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthFailedResponse
            {
                Error = ModelState.Values.SelectMany(e => e.Errors.Select(x => x.ErrorMessage))?.FirstOrDefault()
            });
        }

        var authResponse = await _authService.RegisterAsync(request.Username, request.Password);
        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Error = authResponse.Error
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token,
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var authResponse = await _authService.LoginAsync(request.Username, request.Password);
        if (!authResponse.Success)
        {
            return Ok(new AuthFailedResponse
            {
                Error = authResponse.Error
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Username = request.Username,
            Token = authResponse.Token,
        });
    }

    [HttpPost("facebooklogin")]
    public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginRequest request)
    {
        var authResponse = await _authService.LoginWithFacebookAsync(request.AccessToken);
        if (!authResponse.Success)
        {
            return BadRequest(new FailedResponse
            {
                Error = authResponse.Error
            });
        }

        return Ok(new AuthenticationResult
        {
            Token = authResponse.Token,
        });
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        if (!Request.Cookies.ContainsKey("jwt"))
        {
            return BadRequest(new FailedResponse
            {
                Error = "Some Error Occured"
            });
        }

        var authResponse = await _authService.RefreshTokenAsync(Request.Cookies["jwt"] ?? string.Empty);
        if (!authResponse.Success)
        {
            return BadRequest(new FailedResponse
            {
                Error = authResponse.Error
            });
        }

        return Ok(new AuthenticationResult
        {
            Token = authResponse.Token,
        });
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {

        _authService.LogoutAsync();
        return Ok();
    }
}