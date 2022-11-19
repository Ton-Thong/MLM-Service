using Microsoft.EntityFrameworkCore;
using MlmService.Contracts;
using MlmService.Database.Models.Core;
using MlmService.Services;

namespace MlmService.Database;

public class TenantContext : DbContext
{
    private readonly ITenantService _tenantService;
    private readonly Guid _tenantId;

    public TenantContext(DbContextOptions<TenantContext> options, ITenantService tenantService) : base(options) 
    {
        _tenantService = tenantService;
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Package> Packages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Package>().HasQueryFilter(a => a.TenantId == _tenantId && !a.Deleted);
        modelBuilder.Entity<Member>().HasQueryFilter(a => a.TenantId == _tenantId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = _tenantService.GetConnectionString();
        if(!string.IsNullOrWhiteSpace(tenantConnectionString))
        {
            optionsBuilder.UseNpgsql(tenantConnectionString);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = _tenantService.GetTenantId();
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}