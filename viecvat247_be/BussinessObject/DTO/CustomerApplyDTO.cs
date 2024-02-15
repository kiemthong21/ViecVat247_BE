namespace BussinessObject.DTO
{
    public class CustomerApplyDTO
    {

        public int Aid { get; set; }
        public int ApplicantId { get; set; }
        public string? Cemail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Feedback { get; set; }
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Descrition { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Gender { get; set; }
        public string? CV { get; set; }
        public double? Voting { get; set; }
        public int? Status { get; set; }
    }
}
