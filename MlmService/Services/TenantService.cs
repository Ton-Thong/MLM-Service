using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MlmService.Contracts;
using MlmService.Database;
using MlmService.Extensions;
using MlmService.Options;
using System.Runtime.CompilerServices;

namespace MlmService.Services;

public class TenantService : ITenantService
{
    private readonly HttpContext _httpContext;
    private readonly DbSettings _dbSettings;

    public string ConnectionString { get; set; }
    public Guid TenantId { get; set; }

    public TenantService(IHttpContextAccessor contextAccessor, DbSettings dbSettings)
    {
        _dbSettings = dbSettings;
        _httpContext = contextAccessor.HttpContext;

        TenantId = GetTenantId();
        ConnectionString = GetConnectionString();
    }

    private string GetConnectionString()
    {
        string username = _httpContext.GetUsername();
        if(string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        return _dbSettings.BaseConnection + username;
    }

    private Guid GetTenantId()
    {
        var tenantId = _httpContext.GetTenantId();
        if(!tenantId.HasValue)
        {
            return Guid.Empty;
        }

        return tenantId.Value;
    }
}