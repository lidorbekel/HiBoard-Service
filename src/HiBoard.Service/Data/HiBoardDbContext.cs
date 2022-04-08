﻿using HiBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Service.Data;

public class HiBoardDbContext : DbContext
{
    public HiBoardDbContext(DbContextOptions<HiBoardDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    //create database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
    }
}
