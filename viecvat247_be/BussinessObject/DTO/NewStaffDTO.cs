using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class NewStaffDTO
    {
        [Required(ErrorMessage = "Staff Email is require")]
        public string? Uemail { get; set; }

        [Required(ErrorMessage = "Staff Fullname is require")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Staff PhoneNumber is require")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Staff Address is require")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Staff DOB is require")]
        public DateTime? DOB { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Staff Gender is require")]
        public Boolean Gender { get; set; } = true;
    }
}
