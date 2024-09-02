using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class FloorType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FloorTypeName { get; set; }
        public string? Status { get; set; }
    }
}
