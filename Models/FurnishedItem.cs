using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class FurnishedType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
