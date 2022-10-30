using Newtonsoft.Json;

namespace MlmService.Dto.Auth;

public class AuthSuccessResponse
{
    [JsonProperty("token")]
    public string Token { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
}