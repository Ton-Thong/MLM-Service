using MlmService.Dto.Member;

namespace MlmService.Repository.Interface;

public interface IMemberRepository
{
    Task<Guid> AddMemberAsync(AddMemberDto m, string code, Guid tenantId);
}