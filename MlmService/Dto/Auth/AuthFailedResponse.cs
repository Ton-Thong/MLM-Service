using Newtonsoft.Json;

namespace MlmService.Dto.Auth;

public class AuthFailedResponse
{
    [JsonProperty("error")]
    public string Error { get; set; }
}