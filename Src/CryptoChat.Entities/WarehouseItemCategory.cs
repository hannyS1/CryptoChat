using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

public class WarehouseItemCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }

    // public List<WarehouseItem> Items { get; set; } = new List<WarehouseItem>();
    // public List<User> AccessUsers { get; set; } = new List<User>();

    public override string ToString()
    {
        return Name;
    }
}