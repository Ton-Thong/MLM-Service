using MlmService.Dto;
using MlmService.Dto.Member;

namespace MlmService.Services.Interface;

public interface IMemberService
{
    Task<Response<Guid>> AddMemberAsync(AddMemberDto m, Guid tenantId);
}