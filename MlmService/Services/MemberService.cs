using MlmService.Contracts;
using MlmService.Database.Consts;
using MlmService.Database.Models.Core;
using MlmService.Dto;
using MlmService.Dto.Member;
using MlmService.Repository.Interface;

namespace MlmService.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Response<Guid>> AddMemberAsync(AddMemberDto m)
    {
        var member = new Member(
            code: Guid.NewGuid().ToString()[..6],
            prefix: (Prefix)Convert.ToInt64(m.Prefix),
            gender: (Gender)Convert.ToInt64(m.Gender),
            name: m.Name,
            dateOfBirth: m.DateOfBirth,
            phone: m.Phone,
            email: m.Email,
            line: m.Line,
            facebook: m.Facebook,
            provinceId: m.ProvinceId,
            province: string.Empty,
            amphureId: m.AmphureId,
            amphure: string.Empty,
            districtId: m.DistrictId,
            district: string.Empty,
            address: m.Address,
            zipcode: m.Zipcode
        );
        
        var memberId = await _memberRepository.AddMemberAsync(member);
        return new Response<Guid>
        {
            Succeeded = true,
            Data = memberId,
        };
    }
}