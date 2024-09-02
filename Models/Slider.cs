using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string? FilePath { get; set; }
    }
}
