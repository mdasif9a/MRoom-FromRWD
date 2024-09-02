using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? TypeName { get; set; }
        public string? Status { get; set; }
    }
}
