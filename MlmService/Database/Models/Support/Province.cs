using System.ComponentModel.DataAnnotations;

namespace MlmService.Database.Models.Support;

public class Province
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string NameTh { get; set; }
    public string NameEn { get; set; }
    public string GeographyId { get; set; }
}