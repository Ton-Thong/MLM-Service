using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Database.SharedModels;

public class District
{
    [Key]
    public int Id { get; set; }
    public int Zipcode { get; set; }
    public string NameTh { get; set; }
    public string NameEn { get; set; }
    public int AmphureId { get; set; }
    [ForeignKey(nameof(AmphureId))]
    public Amphure Amphure { get; set; }
}