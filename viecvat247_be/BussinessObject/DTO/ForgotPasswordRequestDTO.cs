using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class ForgotPasswordRequestDTO
    {
        [Required(ErrorMessage = "NewPassword is required"), MaxLength(50)]
        public string? NewPassword { set; get; }

        [Required(ErrorMessage = "Confirm Password is required"), MaxLength(50), Compare("NewPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
