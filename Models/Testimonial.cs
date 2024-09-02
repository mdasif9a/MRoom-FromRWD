using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class Testimonial
    {
        [Key]
        public int Id { get; set; }
        public string? ImgPath { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
