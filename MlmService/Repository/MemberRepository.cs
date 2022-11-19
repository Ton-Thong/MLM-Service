using MlmService.Database;
using MlmService.Database.Models.Core;
using MlmService.Dto.Member;
using MlmService.Repository.Interface;

namespace MlmService.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly TenantContext _tenantContext;

    public MemberRepository(TenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public async Task<Guid> AddMemberAsync(Member member)
    {
        await _tenantContext.Members.AddAsync(member);
        await _tenantContext.SaveChangesAsync();

        return member.Id;
    }
}