using Newtonsoft.Json;

namespace MlmService.Dto.Support;

public class AmphureDto
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
}