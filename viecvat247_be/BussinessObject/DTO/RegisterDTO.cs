using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class RegisterDTO
    {
        public int? roleId { get; set; }
        [Required(ErrorMessage = "Fullname is required"), MaxLength(50)]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Email is required"), MaxLength(50), EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MaxLength(50)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required"), MaxLength(50), Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
