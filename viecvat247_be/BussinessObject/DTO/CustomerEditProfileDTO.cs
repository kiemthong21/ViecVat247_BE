namespace BussinessObject.DTO
{
    public class CustomerEditProfileDTO
    {
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Descrition { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public string? CV { get; set; }
        public bool Gender { get; set; }
        public string[]? Skills { get; set; }
    }
}
