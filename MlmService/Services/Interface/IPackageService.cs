using MlmService.Dto;
using MlmService.Dto.Package;
using MlmService.Pagination;
using MlmService.Pagination.Filter;

namespace MlmService.Services.Interface;

public interface IPackageService
{
    Task<PagedResponse<List<PackageDto>>> GetMembershipsAsync(PaginationFilter paged, FilterMembership filter, Guid tenantId);
    Task<List<PackageForDropdownDto>> GetPackageFoDropdownAsync(Guid tenantId);
    Task<Response<Guid>> CreateMembershipAsync(PackageDto m, Guid tenantId);
    Task<Response<Guid>> UpdateMembershipAsync(PackageDto m, Guid tenantId);
    Task<Response<Guid>> DeleteMembershipAsync(Guid id, Guid tenantId);
}