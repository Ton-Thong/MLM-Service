using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.CoreModels;

public class RefreshToken
{
    [Key]
    public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}