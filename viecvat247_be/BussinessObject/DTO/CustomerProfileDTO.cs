namespace BussinessObject.DTO
{
    public class CustomerProfileDTO
    {

        public int Cid { get; set; }
        public string? Cemail { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Descrition { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? Epoint { get; set; }
        public string? CV { get; set; }
        public bool Gender { get; set; }
        public double? Voting { get; set; }
        public int? Type { get; set; }
        public string? VerifyCode { get; set; }
        public int? FrofileStatus { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<SkillDTO>? Skills { get; set; }

        public List<int> ListSkills { get; set; }
    }
}
