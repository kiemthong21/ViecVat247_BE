using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class ChangePasswordDTO
    {

        [Required(ErrorMessage = "Old Password is required"), MaxLength(50)]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required"), MaxLength(50)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required"), MaxLength(50), Compare("NewPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
