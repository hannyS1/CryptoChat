using CryptoChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }

    public string DbPath => GetDbPath();
    
    public ApplicationContext()
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    private string GetDbPath()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        return Path.Join(path, "application.db"); 
    }

}