using Microsoft.EntityFrameworkCore;
using MlmService.Database;
using MlmService.Dto.Support;

namespace MlmService.Api;

public static class SupportApi
{
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

        support.MapGet("/amphures", async (SharedContext sharedContext) =>
            Results.Ok(await sharedContext.Amphures
                .Select(e => new AmphureDto
                {
                    Id = e.Id,
                    Name = e.NameTh,
                }).ToListAsync())
            ).CacheOutput();

        support.MapGet("/districts",  async (SharedContext sharedContext) =>
            Results.Ok(await sharedContext.Districts
                .Select(e => new DistrictDto
                {
                    Id = e.Id,
                    Name = e.NameTh,
                }).ToListAsync())
            ).CacheOutput();
    }
}