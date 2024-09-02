using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class PropertyCity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? PropertyCityName { get; set; }
        public string? Status { get; set; }
    }
}
