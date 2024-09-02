using System.ComponentModel.DataAnnotations;

namespace MRoom.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your Username")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Select Role")]
        public string? Role { get; set; }
        public bool IsRemember { get; set; }
    }
}
