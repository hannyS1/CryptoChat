using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("user_room")]
public class UserRoom
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("room_id")]
    public int RoomId { get; set; }
    
    public User User { get; set; }
    public Room Room { get; set; }
    
}