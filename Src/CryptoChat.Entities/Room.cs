using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

[Table("room")]
public class Room
{
    [Column("id")]
    public int Id { get; set; }
}