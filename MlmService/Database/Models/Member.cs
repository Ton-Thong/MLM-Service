using System.ComponentModel.DataAnnotations.Schema;
using MlmService.Database.Consts;

namespace MlmService.Database.Models;

public sealed class Member : BaseModel
{
    public Member(string code, Prefix prefix, Gender gender, string firstname, string lastname, DateOnly dateOfBirth, Nationality nationality, string idcard, string phone, string email, string line, string facebook, Guid tenantId)
    {
        Id = Guid.NewGuid();
        Code = code;
        Prefix = prefix;
        Gender = gender;
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        Idcard = idcard;
        Phone = phone;
        Email = email;
        Line = line;
        Facebook = facebook;
        TenantId = tenantId;
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
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Nationality Nationality { get; set; }
    public string Idcard { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Line { get; set; }
    public string Facebook { get; set; }

    //Address
    public string? Address { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? Subdistrict { get; set; }
    public string? Zipcode { get; set; }

    //Payment
    public bool Approved { get; set; }

    public Guid TenantId { get; set; }
    [ForeignKey(nameof(TenantId))]
    public Tenant Tenant { get; set; }

    // public virtual ICollection<Member> Members { get; set; }
    // public Guid MemberShipId { get; set; }
    // [ForeignKey(nameof(MemberShipId))]
    // public Membership Membership { get; set; }
}