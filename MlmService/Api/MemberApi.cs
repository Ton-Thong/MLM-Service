using MlmService.Dto.Member;
using MlmService.Extensions;
using MlmService.Routing;
using MlmService.Services.Interface;

namespace MlmService.Api;

public class MemberApi : IEndpointRouteHandler
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var member = app.MapGroup("/api/member")
            .WithTags("Member")
            .RequireAuthorization();
        
        member.MapPost(string.Empty,  AddMember);
    }

    private static async Task<IResult> AddMember(HttpRequest request, HttpContext context, IMemberService memberService)
    {
        var input = await request.ReadFromJsonAsync<AddMemberDto>();
        var result = await memberService.AddMemberAsync(input, context.GetTenantId());
        return Results.Ok(result);
    }
}