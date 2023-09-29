using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

public class WarehouseItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
    
    [Column("category_id")]
    public int CategoryId { get; set; }
    
    public WarehouseItemCategory Category { get; set; }

    public override string ToString()
    {
        return Name;
    }
}