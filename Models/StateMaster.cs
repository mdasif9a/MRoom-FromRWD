using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRoom.Models
{
    public class StateMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string? Status { get; set; }

        [NotMapped]
        public string? CountryName { get; set; }

    }
}
