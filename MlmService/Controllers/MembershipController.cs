using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MlmService.Dto;
using MlmService.Dto.Member;
using MlmService.Pagination;
using MlmService.Pagination.Filter;
using MlmService.Exceptions;
using MlmService.Extensions;
using MlmService.Helper;
using MlmService.Services;
using MlmService.Services.Interface;
using System.Data;
using System.Net;

namespace MlmService.Controllers;

[Authorize(Roles = AccountHelper.User), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/membership")]
[ApiController]
public class MembershipController : ControllerBase
{
    private readonly IMembershipService _memberShipService;

    public MembershipController(IMembershipService memberShipService)
    {
        _memberShipService = memberShipService;
    }

    [HttpPost("list")]
    [ProducesResponseType(typeof(PagedResponse<List<MembershipDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMemberShips([FromQuery] PaginationFilter paged, [FromBody] FilterMembership filter)
    {
        try
        {
            var result = await _memberShipService.GetMembershipsAsync(paged, filter, HttpContext.GetTenantId());
            return Ok(result);
        }
        catch(PagedException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMembership([FromBody] MembershipDto input)
    {
        var result = await _memberShipService.UpdateMembershipAsync(input, HttpContext.GetTenantId());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMembership([FromBody] MembershipDto input)
    {
        var result = await _memberShipService.CreateMembershipAsync(input, HttpContext.GetTenantId());
        return Created(string.Empty, result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePackage([FromQuery] Guid id)
    {
        var result = await _memberShipService.DeleteMembershipAsync(id, HttpContext.GetTenantId());
        return Ok(result);
    }
}