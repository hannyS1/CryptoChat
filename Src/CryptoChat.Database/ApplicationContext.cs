using System.Reflection;
using CryptoChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<UserRoom> UsersRooms { get; set; }
    public DbSet<WarehouseItem> WarehouseItems { get; set; }
    public DbSet<WarehouseItemCategory> WarehouseItemCategories { get; set; }
    public DbSet<FeatureToggle> FeatureToggles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Rooms)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoom>();
    }
}