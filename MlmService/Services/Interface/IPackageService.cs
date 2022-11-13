using MlmService.Dto;
using MlmService.Dto.Package;
using MlmService.Pagination;
using MlmService.Pagination.Filter;

namespace MlmService.Services.Interface;

public interface IPackageService
{
    Task<PagedResponse<List<PackageDto>>> GetPackagesAsync(PaginationFilter paged, FilterPackage filter, Guid tenantId);
    Task<List<PackageForDropdownDto>> GetPackageForDropdownAsync(Guid tenantId);
    Task<Response<Guid>> CreatePackageAsync(PackageDto m, Guid tenantId);
    Task<Response<Guid>> UpdatePackageAsync(PackageDto m, Guid tenantId);
    Task<Response<Guid>> DeletePackageAsync(Guid id, Guid tenantId);
}