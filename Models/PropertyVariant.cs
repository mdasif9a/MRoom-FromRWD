using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRoom.Models
{
    public class PropertyVariant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PropertyTypeId { get; set; }
        [Required]
        public string? PropertyVariantName { get; set; }
        public string? Status { get; set; }

        [NotMapped]
        public string? PropertyTypeName { get; set; }
    }
}
