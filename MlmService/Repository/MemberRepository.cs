using MlmService.Database;
using MlmService.Database.Models;
using MlmService.Dto.Member;
using MlmService.Repository.Interface;

namespace MlmService.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly DatabaseContext _context;

    public MemberRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddMemberAsync(AddMemberDto m, string code, Guid tenantId)
    {
        var member = new Member(
            code: code,
            prefix: m.Prefix,
            gender: m.Gender,
            firstname: m.Firstname,
            lastname: m.Lastname,
            dateOfBirth: m.DateOfBirth,
            nationality: m.Nationality,
            idcard: m.Idcard,
            phone: m.Phone,
            email: m.Email,
            line: m.Line,
            facebook: m.Facebook,
            tenantId: tenantId
        );

        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();

        return member.Id;
    }
}