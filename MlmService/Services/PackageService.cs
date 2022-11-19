using Microsoft.EntityFrameworkCore;
using MlmService.Database;
using MlmService.Database.Models.Core;
using MlmService.Pagination;
using MlmService.Pagination.Filter;
using MlmService.Dto;
using MlmService.Exceptions;
using MlmService.Dto.Package;
using MlmService.Contracts;

namespace MlmService.Services;

public class PackageService : IPackageService
{
    private readonly TenantContext _tenantContext;

    public PackageService(TenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public async Task<PagedResponse<List<PackageDto>>> GetPackagesAsync(FilterPackage filter)
    {
        if (filter.PageSize > 100)
            throw new PagedException("pagesize cannot more than 100.");

        var membershipQuery = _tenantContext.Packages.AsNoTracking().Where(e => !e.Deleted);

        //Search
        if(!string.IsNullOrWhiteSpace(filter.SearchName))
        {
            membershipQuery = membershipQuery.Where(e => EF.Functions.Like(e.Name.ToLower(), filter.SearchName.ToLower() + "%"));
        }

        if(!string.IsNullOrWhiteSpace(filter.SearchCode))
        {
            membershipQuery = membershipQuery.Where(e => EF.Functions.Like(e.Code.ToLower(), filter.SearchCode.ToLower() + "%"));
        }

        if(!string.IsNullOrWhiteSpace(filter.SearchStatus))
        {
            if(filter.SearchStatus.Equals("Active", StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.Where(e => e.Active);
            }

            if(filter.SearchStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.Where(e => !e.Active);
            }
        }

        if(filter.SearchAmount != null)
        {
            membershipQuery = membershipQuery.Where(e => e.Amount == filter.SearchAmount);
        }

        if(filter.SearchPv != null)
        {
            membershipQuery = membershipQuery.Where(e => e.Pv == filter.SearchPv);
        }

        if(!string.IsNullOrWhiteSpace(filter.SearchStartDate) && !string.IsNullOrWhiteSpace(filter.SearchEndDate))
        {
            var startDate = Convert.ToDateTime(filter.SearchStartDate).ToUniversalTime();
            var endDate = Convert.ToDateTime(filter.SearchEndDate).ToUniversalTime();
            membershipQuery = membershipQuery.Where(e => e.CreatedDate >= startDate && e.CreatedDate <= endDate);
        }

        //Sort
        if(!string.IsNullOrWhiteSpace(filter.SortName))
        {
            if(filter.SortName.Equals(Direction.Ascending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderBy(e => e.Name);
            }

            if(filter.SortName.Equals(Direction.Descending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderByDescending(e => e.Name);
            }
        }

        if(!string.IsNullOrWhiteSpace(filter.SortCode))
        {
            if (filter.SortCode.Equals(Direction.Ascending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderBy(e => e.Code);
            }

            if (filter.SortCode.Equals(Direction.Descending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderByDescending(e => e.Code);
            }
        }

        if(!string.IsNullOrWhiteSpace(filter.SortAmount))
        {
            if (filter.SortAmount.Equals(Direction.Ascending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderBy(e => e.Amount);
            }

            if (filter.SortAmount.Equals(Direction.Descending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderByDescending(e => e.Amount);
            }
        }

        if(!string.IsNullOrWhiteSpace(filter.SortPv))
        {
            if (filter.SortPv.Equals(Direction.Ascending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderBy(e => e.Pv);
            }

            if (filter.SortPv.Equals(Direction.Descending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderByDescending(e => e.Pv);
            }
        }

        if(!string.IsNullOrWhiteSpace(filter.SortDate))
        {
            if (filter.SortDate.Equals(Direction.Ascending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderBy(e => e.CreatedDate);
            }

            if (filter.SortDate.Equals(Direction.Descending, StringComparison.OrdinalIgnoreCase))
            {
                membershipQuery = membershipQuery.OrderByDescending(e => e.CreatedDate);
            }
        }

        var membershipDtoList = await membershipQuery
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(e => new PackageDto
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Amount = e.Amount,
                AmountDisplay = e.Amount.ToString("N2"),
                Pv = e.Pv,
                PvDisplay = e.Pv.ToString("N2"),
                Status = e.Active ? "Active" : "Inactive",
                Date = e.CreatedDate.Date,
            })
            .ToListAsync();

        var totalRecords = await membershipQuery.CountAsync();

        return new PagedResponse<List<PackageDto>>(
            membershipDtoList,
            filter.PageNumber,
            filter.PageSize,
            totalRecords);
    }

    public Task<List<PackageForDropdownDto>> GetPackageForDropdownAsync()
    {


        return _tenantContext.Packages
            .AsNoTracking()
            .Select(e => new PackageForDropdownDto
            {
                Id = e.Id,
                Name = e.Name,
            }).ToListAsync();
    }

    public async Task<Response<Guid>> CreatePackageAsync(PackageDto m)
    {
        var memberShip = new Package(m.Code, m.Name, m.Amount, m.Pv);
        await _tenantContext.AddAsync(memberShip);
        await _tenantContext.SaveChangesAsync();

        return new Response<Guid>
        {
            Data = memberShip.Id,
            Succeeded = true,
        };
    }

    public async Task<Response<Guid>> UpdatePackageAsync(PackageDto m)
    {
        var membership = await _tenantContext.Packages.FindAsync(m.Id);
        if (membership == null)
        {
            return new Response<Guid>
            {
                Succeeded = false,
                Message = "Package not found."
            };
        }

        membership.Name = m.Name;
        membership.Code = m.Code;
        membership.Amount = m.Amount;
        membership.Pv = m.Pv;
        membership.Active = m.Status == "Active";

        await _tenantContext.SaveChangesAsync();

        return new Response<Guid>
        {
            Succeeded = true,
        };
    }

    public async Task<Response<Guid>> DeletePackageAsync(Guid id)
    {
        var membership = await _tenantContext.Packages.FindAsync(id);
        if(membership == null)
        {
            return new Response<Guid>
            {
                Succeeded = false,
                Message = "Package not found."
            };
        }

        membership.Deleted = true;
        await _tenantContext.SaveChangesAsync();

        return new Response<Guid>
        {
            Succeeded = true,
        };
    }
}