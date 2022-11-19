using Microsoft.EntityFrameworkCore;
using MlmService.Database.Models.Core;
using MlmService.Database.Models.Support;

namespace MlmService.Database;

public class CoreContext : DbContext
{
    public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Amphure> Amphures { get; set; }
    public DbSet<District> Districts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}