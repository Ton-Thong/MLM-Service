using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.Models;

public class Package : BaseModel
{
    public Package(string code, string name, decimal amount, decimal pv, Guid tenantId)
    {
        Id = Guid.NewGuid();

        Code = code;
        Name = name;
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Pv = decimal.Round(pv, 2, MidpointRounding.AwayFromZero);
        TenantId = tenantId;
        Active = true;

        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }

    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public decimal Pv { get; set; }
    public bool Active { get; set; }
    public bool Deleted { get; set; }
    public Guid TenantId { get; set; }
    [ForeignKey(nameof(TenantId))]
    public Tenant Tenant { get; set; }
}