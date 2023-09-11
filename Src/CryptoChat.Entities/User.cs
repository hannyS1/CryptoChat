using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("user")]
public class User
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
}
