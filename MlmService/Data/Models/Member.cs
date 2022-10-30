using MlmService.Data.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlmService.Data.Models;

public class Member : BaseModel
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string IdCard { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Position Position { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public bool Approved { get; set; }
    public bool Active { get; set; }
    public Guid SponsorId { get; set; }
    [ForeignKey(nameof(SponsorId))]
    public Member Sponsor { get; set; }
    public virtual ICollection<Member> Members { get; set; }
    public Guid MemberShipId { get; set; }
    [ForeignKey(nameof(MemberShipId))]
    public Membership Membership { get; set; }
    public Guid TenantId { get; set; }
    [ForeignKey(nameof(TenantId))]
    public Tenant Tenant { get; set; }
}