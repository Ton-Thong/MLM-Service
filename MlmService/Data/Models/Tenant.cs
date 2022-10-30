using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Data.Models;

public class Tenant
{
    [Key]
    public Guid Id { get; set; }
    public virtual ICollection<User> Users { get; set; }
}