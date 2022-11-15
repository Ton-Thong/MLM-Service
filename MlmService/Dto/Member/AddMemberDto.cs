using MlmService.Database.Consts;
using Newtonsoft.Json;

namespace MlmService.Dto.Member;

public class AddMemberDto
{
    public string Sponsor { get; set; }
    public string Upline { get; set; }
    public string Position { get; set; }
    public Guid Package { get; set; }
    public string Prefix { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int ProvinceId { get; set; }
    public int AmphureId { get; set; }
    public int DistrictId { get; set; }
    public int Zipcode { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Line { get; set; }
    public string Facebook { get; set; }
    
}