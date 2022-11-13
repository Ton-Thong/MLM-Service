using Microsoft.EntityFrameworkCore;
using MlmService.Database;
using MlmService.Dto.Support;

namespace MlmService.Api;

public static class SupportApi
{
    private static readonly Dictionary<int, List<AmphureDto>> AmphureCache = new();
    private static readonly Dictionary<int, List<DistrictDto>> DistrictCache = new();
    
    public static void MapSupportRoutes(this IEndpointRouteBuilder app)
    {
        var support = app
            .MapGroup("/api/support")
            .WithTags("Support")
            .RequireAuthorization();

        support.MapGet("/provinces",  async (SharedContext sharedContext) => 
            Results.Ok(await sharedContext.Provinces
                .Select(e => new ProvinceDto
                {
                    Id = e.Id,
                    Name = e.NameTh,
                }).ToListAsync())
            ).CacheOutput();

        support.MapGet("/amphures", async (int provinceId, SharedContext sharedContext) => 
        {
            if (AmphureCache.ContainsKey(provinceId))
            {
                return Results.Ok(AmphureCache[provinceId]);
            }

            var amphures = await sharedContext.Amphures
                .Where(e => e.ProvinceId == provinceId)
                .Select(e => new AmphureDto
                {
                    Id = e.Id,
                    Name = e.NameTh,
                }).ToListAsync();

            AmphureCache[provinceId] = amphures;
            return Results.Ok(amphures);
        });

        support.MapGet("/districts", async (int amphureId, SharedContext sharedContext) =>
        {
            if (DistrictCache.ContainsKey(amphureId))
            {
                return Results.Ok(DistrictCache[amphureId]);
            }

            var districts = await sharedContext.Districts
                .Where(e => e.AmphureId == amphureId)
                .Select(e => new DistrictDto
                {
                    Id = e.Id,
                    Name = e.NameTh,
                }).ToListAsync();

            DistrictCache[amphureId] = districts;
            return Results.Ok(districts);
        });
    }
}