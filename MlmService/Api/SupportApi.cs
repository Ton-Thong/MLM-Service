using Microsoft.EntityFrameworkCore;
using MlmService.Database;
using MlmService.Dto.Support;
using MlmService.Routing;

namespace MlmService.Api;

public class SupportApi : IEndpointRouteHandler
{
    private static readonly Dictionary<int, List<AmphureDto>> AmphureCache = new();
    private static readonly Dictionary<int, List<DistrictDto>> DistrictCache = new();
    
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var support = app
            .MapGroup("/api/support/")
            .WithTags("Support")
            .RequireAuthorization();

        support.MapGet("provinces",  GetProvinces).CacheOutput().Produces<ProvinceDto>();
        support.MapGet("amphures", GetAmphures).Produces<AmphureDto>();
        support.MapGet("districts", GetDistricts).Produces<DistrictDto>();
    }

    private static async Task<IResult> GetProvinces(CoreContext coreContext)
    {
        var provinces = await coreContext.Provinces
            .Select(e => new ProvinceDto
            {
                Id = e.Id,
                Name = e.NameTh,
            }).ToListAsync();
        
        return Results.Ok(provinces);
    }

    private static async Task<IResult> GetAmphures(int provinceId, CoreContext coreContext)
    {
        if (AmphureCache.ContainsKey(provinceId))
        {
            return Results.Ok(AmphureCache[provinceId]);
        }

        var amphures = await coreContext.Amphures
            .Where(e => e.ProvinceId == provinceId)
            .Select(e => new AmphureDto
            {
                Id = e.Id,
                Name = e.NameTh,
            }).ToListAsync();

        AmphureCache[provinceId] = amphures;
        return Results.Ok(amphures);
    }

    private static async Task<IResult> GetDistricts(int amphureId, CoreContext coreContext)
    {
        if (DistrictCache.ContainsKey(amphureId))
        {
            return Results.Ok(DistrictCache[amphureId]);
        }

        var districts = await coreContext.Districts
            .Where(e => e.AmphureId == amphureId)
            .Select(e => new DistrictDto
            {
                Id = e.Id,
                Name = e.NameTh,
                Zipcode = e.Zipcode,
            }).ToListAsync();

        DistrictCache[amphureId] = districts;
        return Results.Ok(districts);
    }
}