using MlmService.Dto;
using MlmService.Dto.Member;
using MlmService.Repository.Interface;
using MlmService.Services.Interface;

namespace MlmService.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Response<Guid>> AddMemberAsync(AddMemberDto m, Guid tenantId)
    {
        string code = Guid.NewGuid().ToString()[..6];
        var memberId = await _memberRepository.AddMemberAsync(m, code, tenantId);

        return new Response<Guid>
        {
            Succeeded = true,
            Data = memberId,
        };
    }
}