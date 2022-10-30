﻿using MlmService.Data.Models;
using MlmService.Data;
using MlmService.Dto.Member;
using Microsoft.EntityFrameworkCore;
using MlmService.Services.Interface;
using MlmService.Exceptions;
using MlmService.Pagination;
using MlmService.Pagination.Filter;
using MlmService.Dto;

namespace MlmService.Services;

public class MembershipService : IMembershipService
{
    private readonly ApplicationDbContext _context;

    public MembershipService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<List<MembershipDto>>> GetMembershipsAsync(PaginationFilter paged, FilterMembership filter ,Guid tenantId)
    {
        if (paged.PageSize > 100)
            throw new PagedException("pagesize cannot more than 100.");

        var membershipQuery = _context.Memberships
            .AsNoTracking()
            .Where(e => e.TenantId == tenantId && !e.Deleted);

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
            .Skip((paged.PageNumber - 1) * paged.PageSize)
            .Take(paged.PageSize)
            .Select(e => new MembershipDto
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

        var pagedResponse = new PagedResponse<List<MembershipDto>>(
            membershipDtoList,
            paged.PageNumber,
            paged.PageSize, 
            totalRecords);

        return pagedResponse;
    }

    public async Task<Response<Guid>> CreateMembershipAsync(MembershipDto m, Guid tenantId)
    { 
        var memberShip = new Membership(m.Code, m.Name, m.Amount, m.Pv, tenantId);
        await _context.AddAsync(memberShip);
        await _context.SaveChangesAsync();

        return new Response<Guid>
        {
            Data = memberShip.Id,
            Succeeded = true,
        };
    }

    public async Task<Response<Guid>> UpdateMembershipAsync(MembershipDto m, Guid tenantId)
    {
        var membership = await _context.Memberships.FirstOrDefaultAsync(e => e.Id == m.Id && e.TenantId == tenantId && !e.Deleted);
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

        await _context.SaveChangesAsync();

        return new Response<Guid>
        {
            Succeeded = true,
        };
    }

    public async Task<Response<Guid>> DeleteMembershipAsync(Guid id, Guid tenantId)
    {
        var membership = await _context.Memberships.FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId && !e.Deleted);
        if(membership == null)
        {
            return new Response<Guid>
            {
                Succeeded = false,
                Message = "Package not found."
            };
        }

        membership.Deleted = true;
        await _context.SaveChangesAsync();

        return new Response<Guid>
        {
            Succeeded = true,
        };
    }
}