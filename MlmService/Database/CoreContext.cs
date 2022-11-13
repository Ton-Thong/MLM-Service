using Microsoft.EntityFrameworkCore;
using MlmService.Database.CoreModels;

namespace MlmService.Database;

public class CoreContext : DbContext
{
    public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Package> Packages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}