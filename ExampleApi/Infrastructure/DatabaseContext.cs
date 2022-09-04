using System.Reflection;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DatabaseContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}