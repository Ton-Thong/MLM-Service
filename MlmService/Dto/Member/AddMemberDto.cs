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
    [JsonProperty("address")]
    public string Address { get; set; }
    [JsonProperty("provinceId")]
    public int ProvinceId { get; set; }
    [JsonProperty("province")]
    public string Province { get; set; }
    [JsonProperty("amphureId")]
    public int AmphureId { get; set; }
    [JsonProperty("amphure")]
    public string Amphure { get; set; }
    [JsonProperty("districtId")]
    public int DistrictId { get; set; }
    [JsonProperty("district")]
    public string District { get; set; }
    [JsonProperty("zipcode")]
    public int Zipcode { get; set; }
}