using MlmService.Dto;
using MlmService.Dto.Member;
using MlmService.Pagination;
using MlmService.Pagination.Filter;

namespace MlmService.Services.Interface;

public interface IMembershipService
{
    Task<PagedResponse<List<MembershipDto>>> GetMembershipsAsync(PaginationFilter paged, FilterMembership filter, Guid tenantId);
    Task<Response<Guid>> CreateMembershipAsync(MembershipDto m, Guid tenantId);
    Task<Response<Guid>> UpdateMembershipAsync(MembershipDto m, Guid tenantId);
    Task<Response<Guid>> DeleteMembershipAsync(Guid id, Guid tenantId);
}