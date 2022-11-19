using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.Models.Support;

public class Amphure
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string NameTh { get; set; }
    public string NameEn { get; set; }
    public int ProvinceId { get; set; }
    [ForeignKey(nameof(ProvinceId))]
    public Province Province { get; set; }
}
