using System.ComponentModel.DataAnnotations;

namespace MlmService.Database.CoreModels;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}