using Microsoft.EntityFrameworkCore;
using MlmService.Database.SharedModels;

namespace MlmService.Database;

public class SharedContext : DbContext
{
    public SharedContext(DbContextOptions<SharedContext> options) : base(options) { }

    public DbSet<Province> Provinces { get; set; }
    public DbSet<Amphure> Amphures { get; set; }
    public DbSet<District> Districts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
} 