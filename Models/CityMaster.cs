using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRoom.Models
{
    public class CityMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string? Status { get; set; }


        [NotMapped]
        public string? StateName { get; set; }
        [NotMapped]
        public string? CountryName { get; set; }
    }
}
