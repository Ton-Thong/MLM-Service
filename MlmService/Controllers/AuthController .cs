using Microsoft.AspNetCore.Mvc;
using MlmService.Dto.Auth;
using MlmService.Services.Interface;

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

        return Ok(new AuthenticationResult
        {
            Success = true,
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

        return Ok(new AuthenticationResult
        {
            Success = true,
            Token = authResponse.Token,
        });
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        if (!Request.Cookies.ContainsKey("jwt"))
        {
            return BadRequest(new AuthFailedResponse
            {
                Error = "Some Error Occured"
            });
        }

        var authResponse = await _authService.RefreshTokenAsync(Request.Cookies["jwt"] ?? string.Empty);
        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Error = authResponse.Error
            });
        }

        return Ok(new AuthenticationResult
        {
            Success = true,
            Token = authResponse.Token,
        });
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        _authService.Logout();
        return Ok();
    }
}