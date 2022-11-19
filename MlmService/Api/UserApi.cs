using MlmService.Contracts;
using MlmService.Extensions;

namespace MlmService.Api;

public class UserApi
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var member = app.MapGroup("/api/user")
            .WithTags("User")
            .RequireAuthorization();

        member.MapGet("userinfo", GetUserInfo);
    }

    private static async Task<IResult> GetUserInfo(IUserService userService, HttpContext context)
    {
        var user = await userService.GetUserInfo(context.GetUserId());
        return Results.Ok(user);
    }
}