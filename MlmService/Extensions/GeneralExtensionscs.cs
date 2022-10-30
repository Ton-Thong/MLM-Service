using MlmService.Dto.User;

namespace MlmService.Extensions;

public static class GeneralExtensions
{
    public static UserJwtDto GetUserJwt(this HttpContext httpContext)
    {
        return new UserJwtDto
        {
            Id = httpContext.GetUserId(),
            TenantId = httpContext.GetTenantId(),
        };
    }

    public static Guid GetUserId(this HttpContext httpContext)
    {
        var id = httpContext.User.Claims.Single(e => e.Type == "id").Value;
        return new Guid(id);
    }

    public static Guid GetTenantId(this HttpContext httpContext)
    {
        var tenantId = httpContext.User.Claims.Single(e => e.Type == "tenantId").Value;
        return new Guid(tenantId);
    }
}