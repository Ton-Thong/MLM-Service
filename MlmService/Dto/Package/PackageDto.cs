using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MlmService.Dto.Package;

public class PackageDto
{
    public Guid? Id { get; set; }
    [Required]
    [JsonProperty("code")]
    public string Code { get; set; }
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }
    [Required]
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    [JsonProperty("amountDisplay")]
    public string? AmountDisplay { get; set; }
    [Required]
    [JsonProperty("pv")]
    public decimal Pv { get; set; }
    [JsonProperty("pvDisplay")]
    public string? PvDisplay { get; set; }
    [Required]
    public string Status { get; set; }
    [JsonProperty("date")]
    public DateTime? Date { get; set; }
}