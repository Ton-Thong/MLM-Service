namespace MlmService.Extensions;

public static class ClaimExtensions
{
    public static Guid GetUserId(this HttpContext httpContext)
    {
        var id = httpContext.User.Claims.Single(e => e.Type == "id").Value;
        return new Guid(id);
    }

    public static Guid? GetTenantId(this HttpContext httpContext)
    {
        var tenantId = httpContext.User.Claims.FirstOrDefault(e => e.Type == "tenantId");
        if(tenantId != null && !string.IsNullOrWhiteSpace(tenantId.Value))
        {
            return new Guid(tenantId.Value);
        }

        return null;
    }

    public static string GetUsername(this HttpContext httpContext)
    {
        var user = httpContext.User.Claims.FirstOrDefault(e => e.Type == "user");
        if(user != null && !string.IsNullOrWhiteSpace(user.Value))
        {
            return user.Value;
        }

        return null;
    }
}