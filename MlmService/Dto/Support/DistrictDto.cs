using Newtonsoft.Json;

namespace MlmService.Dto.Support;

public class DistrictDto
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("amphureId")]
    public int AmphureId { get; set; }
    [JsonProperty("zipcode")]
    public int Zipcode { get; set; }
}