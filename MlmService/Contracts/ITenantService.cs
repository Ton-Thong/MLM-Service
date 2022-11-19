namespace MlmService.Contracts;

public interface ITenantService
{
    public string ConnectionString { get; set; }
    public Guid TenantId { get; set; }
}