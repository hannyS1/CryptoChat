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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}