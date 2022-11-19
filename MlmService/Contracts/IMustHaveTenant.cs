namespace MlmService.Contracts;

public interface IMustHaveTenant
{
    public Guid TenantId { get; set; }
}