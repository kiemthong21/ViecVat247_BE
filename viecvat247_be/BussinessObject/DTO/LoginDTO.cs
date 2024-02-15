using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Gmail is required"), MaxLength(50)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MaxLength(50)]
        public string? Password { get; set; }
    }
}
