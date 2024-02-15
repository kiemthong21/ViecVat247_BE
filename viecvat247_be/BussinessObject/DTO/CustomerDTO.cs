namespace BussinessObject.DTO
{
    public class CustomerDTO
    {
        public int Cid { get; set; }
        public string? Cemail { get; set; }
        public int? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Descrition { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? Epoint { get; set; }
        public bool Gender { get; set; }
        public string? CV { get; set; }
        public double? Voting { get; set; }
        public int? Type { get; set; }

        public int? Status { get; set; }
    }
}
