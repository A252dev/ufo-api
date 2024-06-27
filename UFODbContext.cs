using Microsoft.EntityFrameworkCore;
using ufo_api.Models;

public class UFODbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserBalance> UserBalances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=127.0.0.1;user=root;password=password;database=ufopay";

        optionsBuilder.UseMySql(connectionString: connectionString, serverVersion: ServerVersion.AutoDetect(connectionString: connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}