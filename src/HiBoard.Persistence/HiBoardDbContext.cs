using HiBoard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Persistence;

public class HiBoardDbContext : DbContext
{
    public HiBoardDbContext(DbContextOptions<HiBoardDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Company> Companies => Set<Company>();

    //create database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
    }
}
