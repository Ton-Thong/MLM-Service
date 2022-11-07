using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MlmService.Dto;
using MlmService.Dto.Member;
using MlmService.Extensions;
using MlmService.Helper;
using MlmService.Services.Interface;

namespace MlmService.Controllers;

[Authorize(Roles = AccountHelper.User), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/member")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddMember([FromBody] AddMemberDto input)
    {
        var result = await _memberService.AddMemberAsync(input, HttpContext.GetTenantId());
        return Created(string.Empty, result);
    }
}