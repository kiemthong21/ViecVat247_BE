using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class StaffUpdateDTO
    {

        [Required(ErrorMessage = "Staff Fullname is require")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Staff PhoneNumber is require")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Staff address is require")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Staff avatar is require")]
        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Staff gender is require")]
        public DateTime? Dob { get; set; }
        public bool Gender { get; set; }
    }
}
