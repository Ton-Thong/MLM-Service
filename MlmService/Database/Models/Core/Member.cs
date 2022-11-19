using System.ComponentModel.DataAnnotations.Schema;
using MlmService.Contracts;
using MlmService.Database.Consts;

namespace MlmService.Database.Models.Core;

public sealed class Member : BaseModel, IMustHaveTenant
{
    public Member(string code, Prefix prefix, Gender gender, string name, DateOnly dateOfBirth, string phone, string email, string line, string facebook,
        int? provinceId, string province, int? amphureId, string amphure, int? districtId, string district, string address, int? zipcode)
    {
        Id = Guid.NewGuid();
        Code = code;
        Prefix = prefix;
        Gender = gender;
        Name = name;
        DateOfBirth = dateOfBirth;
        Phone = phone;
        Email = email;
        Line = line;
        Facebook = facebook;
        ProvinceId = provinceId;
        Province = province;
        AmphureId = amphureId;
        Amphure = amphure;
        DistrictId = districtId;
        District = district;
        Address = address;
        Zipcode = zipcode;
        Approved = false;
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }

    //Network
    public string? SponsorCode { get; set; }
    public Guid? SponsorId { get; set; }
    [ForeignKey(nameof(SponsorId))]
    public Member? Sponsor { get; set; }
    public Guid? MembershipId { get; set; }
    [ForeignKey(nameof(MembershipId))]
    public Package? Membership { get; set; }

    //Contact
    public string Code { get; set; }
    public Prefix Prefix { get; set; }
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Line { get; set; }
    public string Facebook { get; set; }

    //Address
    public int? ProvinceId { get; set; }
    public string? Province { get; set; }
    public int? AmphureId { get; set; }
    public string? Amphure { get; set; }
    public int? DistrictId { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
    public int? Zipcode { get; set; }

    //Payment
    public bool Approved { get; set; }

    public Guid TenantId { get; set; }

    // public virtual ICollection<Member> Members { get; set; }
    // public Guid MemberShipId { get; set; }
    // [ForeignKey(nameof(MemberShipId))]
    // public Membership Membership { get; set; }
}