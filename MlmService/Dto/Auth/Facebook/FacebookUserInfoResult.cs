using Newtonsoft.Json;

namespace MlmService.Dto.Authentication.Facebook;

public partial class FacebookUserInfoResult
{
    [JsonProperty("id")]
    public string UserId { get; set; }
    [JsonProperty("first_name")]
    public string Firstname { get; set; }
    [JsonProperty("last_name")]
    public string Lastname { get; set; }
    [JsonProperty("picture")]
    public Picture Picture { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
}

public partial class Picture
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}

public partial class Data
{
    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("isSilhouette")]
    public bool IsSilhouette { get; set; }
    [JsonProperty("url")]
    public Uri Url { get; set; }
    [JsonProperty("width")]
    public long Width { get; set; }
}