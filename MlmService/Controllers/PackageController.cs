using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MlmService.Dto;
using MlmService.Exceptions;
using MlmService.Pagination;
using MlmService.Pagination.Filter;
using MlmService.Extensions;
using MlmService.Helper;
using MlmService.Services.Interface;
using MlmService.Dto.Package;

namespace MlmService.Controllers;

[Authorize(Roles = AccountHelper.User), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/package")]
[ApiController]
public class PackageController : ControllerBase
{
    private readonly IPackageService _packageService;

    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpPost("list")]
    [ProducesResponseType(typeof(PagedResponse<List<PackageDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMemberShips([FromQuery] PaginationFilter paged, [FromBody] FilterMembership filter)
    {
        try
        {
            var result = await _packageService.GetMembershipsAsync(paged, filter, HttpContext.GetTenantId());
            return Ok(result);
        }
        catch(PagedException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("package-dropdown")]
    [ProducesResponseType(typeof(List<PackageForDropdownDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPackageForDropdown()
    {
        var result = await _packageService.GetPackageFoDropdownAsync(HttpContext.GetTenantId());
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMembership([FromBody] PackageDto input)
    {
        var result = await _packageService.UpdateMembershipAsync(input, HttpContext.GetTenantId());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMembership([FromBody] PackageDto input)
    {
        var result = await _packageService.CreateMembershipAsync(input, HttpContext.GetTenantId());
        return Created(string.Empty, result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Response<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePackage([FromQuery] Guid id)
    {
        var result = await _packageService.DeleteMembershipAsync(id, HttpContext.GetTenantId());
        return Ok(result);
    }
}