using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("user")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("category_id")]
    public int AllowedCategoryId { get; set; }

    public List<Room> Rooms { get; set; } = new List<Room>();
    public List<UserRoom> UsersRooms { get; set; } = new List<UserRoom>();
    
    public WarehouseItemCategory AllowedCategory { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
