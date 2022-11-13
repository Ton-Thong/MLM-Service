using MlmService.Dto.Auth;
using MlmService.Services.Interface;

namespace MlmService.Api;

public static class AuthApi
{
    public static void MapAuthRoutes(this IEndpointRouteBuilder app)
    {
        var auth = app.MapGroup("/api/auth")
            .WithTags("Auth")
            .AllowAnonymous();

        auth.MapPost("/register", async (UserRegistrationRequest request, IAuthService authService) =>
        {
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
        });

        auth.MapPost("/login", async (UserLoginRequest request, IAuthService authService) =>
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
        });

        auth.MapGet("/refresh", async (HttpContext context, IAuthService authService) =>
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
        });

        auth.MapGet("logout", (IAuthService authService) =>
        {
            authService.Logout();
            return Results.Ok();
        });
    }
}