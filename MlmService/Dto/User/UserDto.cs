using Newtonsoft.Json;

namespace MlmService.Dto.User;


public class UserDto
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
}