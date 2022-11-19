using MlmService.Dto;
using MlmService.Dto.Member;

namespace MlmService.Contracts;

public interface IMemberService
{
    Task<Response<Guid>> AddMemberAsync(AddMemberDto m);
}