using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.CoreModels;

public sealed class User : BaseModel
{
    public string Username { get; set; }
    public string? Salt { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public bool IsUsernameLogin { get; set; }
    public bool IsRoot { get; set; }
    public Guid TenantId { get; set; }
    [ForeignKey(nameof(TenantId))]
    public Tenant Tenant { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}