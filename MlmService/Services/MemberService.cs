using MlmService.Database.CoreModels;
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
        var member = new Member(
            code: Guid.NewGuid().ToString()[..6],
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
            provinceId: m.ProvinceId,
            province: m.Province,
            amphureId: m.AmphureId,
            amphure: m.Amphure,
            districtId: m.DistrictId,
            district: m.District,
            address: m.Address,
            zipcode: m.Zipcode,
            tenantId: tenantId
        );
        
        var memberId = await _memberRepository.AddMemberAsync(member);
        return new Response<Guid>
        {
            Succeeded = true,
            Data = memberId,
        };
    }
}