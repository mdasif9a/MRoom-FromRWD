using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class Electricity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
