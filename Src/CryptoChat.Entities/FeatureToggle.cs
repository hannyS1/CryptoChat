using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoChat.Entities;

public class FeatureToggle
{
    [Key]
    public string Key { get; set; }
    
    [Required]
    public bool Enabled { get; set; }
}