using MlmService.Database.CoreModels;
using MlmService.Dto.Member;

namespace MlmService.Repository.Interface;

public interface IMemberRepository
{
    Task<Guid> AddMemberAsync(Member member);
}