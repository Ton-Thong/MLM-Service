using MlmService.Database.Consts;
using Newtonsoft.Json;

namespace MlmService.Dto.Member;

public class AddMemberDto
{
    [JsonProperty("prefix")]
    public Prefix Prefix { get; set; }
    [JsonProperty("gender")]
    public Gender Gender { get; set; }
    [JsonProperty("firstname")]
    public string Firstname { get; set; }
    [JsonProperty("lastname")]
    public string Lastname { get; set; }
    [JsonProperty("dateOfBirth")]
    public DateOnly DateOfBirth { get; set; }
    [JsonProperty("nationality")]
    public Nationality Nationality { get; set; }
    [JsonProperty("idcard")]
    public string Idcard { get; set; }
    [JsonProperty("phone")]
    public string Phone { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("line")]
    public string Line { get; set; }
    [JsonProperty("facebook")]
    public string Facebook { get; set; }
}