using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("message")]
public class Message
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("text")]
    public string Text { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("room_id")]
    public int RoomId { get; set; }

    public User User { get; set; }
    public Room Room { get; set; }

    public override string ToString()
    {
        return $"{User.Name}: {Text}";
    }
}