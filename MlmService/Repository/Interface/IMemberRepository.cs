using MlmService.Database.Models.Core;
using MlmService.Dto.Member;

namespace MlmService.Repository.Interface;

public interface IMemberRepository
{
    Task<Guid> AddMemberAsync(Member member);
}