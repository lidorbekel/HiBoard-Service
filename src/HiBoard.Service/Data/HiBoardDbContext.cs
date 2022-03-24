using Microsoft.EntityFrameworkCore;

namespace HiBoard.Service.Data;

public class HiBoardDbContext : DbContext
{
    public HiBoardDbContext(DbContextOptions<HiBoardDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contact).Assembly);
    }
}
