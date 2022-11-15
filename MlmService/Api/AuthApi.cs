using MlmService.Routing;
using MlmService.Dto.Auth;
using MlmService.Services.Interface;

namespace MlmService.Api;

public class AuthApi : IEndpointRouteHandler
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var auth = app.MapGroup("/api/auth/").WithTags("Auth");
        auth.MapPost("register", Register);
        auth.MapPost("login", Login);
        auth.MapGet("refresh", Refresh);
        auth.MapGet("logout", Logout);
    }
    
    private static async Task<IResult> Register(UserLoginRequest request, IAuthService authService) {
        var response = await authService.RegisterAsync(request.Username, request.Password);
        if (!response.Success)
        {
            return Results.BadRequest(new AuthFailedResponse
            {
                Error = response.Error
            });
        }

        return Results.Ok(new AuthenticationResult
        {
            Success = true,
            Token = response.Token,
        });
    }

    private static async Task<IResult> Login(UserLoginRequest request, IAuthService authService)
    {
        var response = await authService.LoginAsync(request.Username, request.Password);
        if (!response.Success)
        {
            return Results.Ok(new AuthFailedResponse
            {
                Error = response.Error
            });
        }
            
        return Results.Ok(new AuthenticationResult
        {
            Success = true,
            Token = response.Token,
        });
    }

    private static async Task<IResult> Refresh(HttpContext context, IAuthService authService)
    {
        if (!context.Request.Cookies.ContainsKey("jwt"))
        {
            return Results.BadRequest(new AuthFailedResponse
            {
                Error = "Some Error Occured"
            });
        }

        var response = await authService.RefreshTokenAsync(context.Request.Cookies["jwt"] ?? string.Empty);
        if (!response.Success)
        {
            return Results.BadRequest(new AuthFailedResponse
            {
                Error = response.Error
            });
        }
            
        return Results.Ok(new AuthenticationResult
        {
            Success = true,
            Token = response.Token,
        });
    }

    private static IResult Logout(IAuthService authService)
    {
        authService.Logout();
        return Results.Ok();
    }
}