namespace BussinessObject.DTO
{
    public class StaffDTO
    {
        public int Uid { get; set; }
        public string? Uemail { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime? Dob { get; set; }
        public int? Status { get; set; }
        public virtual List<TypeManagerDTO>? TypeManagers { get; set; }
        public List<int>? ListTypeManagers { get; set; }
    }
}
