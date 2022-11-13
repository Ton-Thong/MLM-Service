using MlmService.Database;
using MlmService.Database.CoreModels;
using MlmService.Dto.Member;
using MlmService.Repository.Interface;

namespace MlmService.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly CoreContext _context;

    public MemberRepository(CoreContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddMemberAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();

        return member.Id;
    }
}