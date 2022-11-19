using MlmService.Contracts;
using MlmService.Dto.Package;
using MlmService.Exceptions;
using MlmService.Pagination.Filter;
using MlmService.Routing;
using MlmService.Services;

namespace MlmService.Api;

public class PackageApi : IEndpointRouteHandler
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var package = app.MapGroup("/api/package/")
            .WithTags("Package")
            .RequireAuthorization();

        package.MapPost("list", GetPackages);
        package.MapGet("package-dropdown", GetPackageForDropdown);
        package.MapPost(string.Empty, CreatePackage);
        package.MapPut(string.Empty, UpdatePackage);
        package.MapDelete(string.Empty, DeletePackage);
    }

    private static async Task<IResult> GetPackages(FilterPackage filter, IPackageService packageService)
    {
        try
        {
            var result = await packageService.GetPackagesAsync(filter);
            return Results.Ok(result);
        }
        catch(PagedException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> GetPackageForDropdown(IPackageService packageService)
    {
        var result = await packageService.GetPackageForDropdownAsync();
        return Results.Ok(result);
    }

    private static async Task<IResult> CreatePackage(PackageDto input, IPackageService packageService)
    {
        var result = await packageService.CreatePackageAsync(input);
        return Results.Created(string.Empty, result);
    }

    private static async Task<IResult> UpdatePackage(PackageDto input, IPackageService packageService)
    {
        var result = await packageService.UpdatePackageAsync(input);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeletePackage(Guid id, IPackageService packageService)
    {
        var result = await packageService.DeletePackageAsync(id);
        return Results.Created(string.Empty, result);
    }
}