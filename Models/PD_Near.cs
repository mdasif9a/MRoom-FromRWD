using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRoom.Models
{
    public class PD_Near
    {
        [Key]
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int NearById { get; set; }
    }
}
