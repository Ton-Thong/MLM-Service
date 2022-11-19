namespace MlmService.Extensions;

public static class ClaimExtensions
{
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

    public static string GetUsername(this HttpContext httpContext)
    {
        var sub =  httpContext.User.Claims.Single(e => e.Type == "user").Value;
        return sub;
    }
}