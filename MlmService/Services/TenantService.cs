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

    private string ConnectionString { get; set; }

    public TenantService(IHttpContextAccessor contextAccessor, DbSettings dbSettings)
    {
        _dbSettings = dbSettings;
        _httpContext = contextAccessor.HttpContext;
    }

    public string GetConnectionString()
    {
        if (!string.IsNullOrWhiteSpace(ConnectionString))
            return ConnectionString;

        string username = _httpContext.GetUsername();
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Invalid Tenant!");

        return _dbSettings.BaseConnection + username;
    }

    public Guid GetTenantId()
    {

        return _httpContext.GetTenantId();
    }

    public void SetConnectionString(string connectionString)
    {
        ConnectionString = connectionString;
    }
}