using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("room")]
public class Room
{
    [Column("id")]
    public int Id { get; set; }

    public List<User> Users { get; set; } = new List<User>();
    public List<UserRoom> UsersRooms { get; set; } = new List<UserRoom>();

    public override string ToString()
    {
        return string.Join(',', Users);
    }
}