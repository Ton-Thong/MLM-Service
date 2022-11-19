namespace MlmService.Contracts;

public interface ITenantService
{
    public string GetConnectionString();
    public Guid GetTenantId();
    public void SetConnectionString(string connectionString);
}