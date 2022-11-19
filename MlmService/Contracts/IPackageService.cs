using MlmService.Dto;
using MlmService.Dto.Package;
using MlmService.Pagination;
using MlmService.Pagination.Filter;

namespace MlmService.Contracts;

public interface IPackageService
{
    Task<PagedResponse<List<PackageDto>>> GetPackagesAsync(PaginationFilter paged, FilterPackage filter);
    Task<List<PackageForDropdownDto>> GetPackageForDropdownAsync();
    Task<Response<Guid>> CreatePackageAsync(PackageDto m);
    Task<Response<Guid>> UpdatePackageAsync(PackageDto m);
    Task<Response<Guid>> DeletePackageAsync(Guid id);
}