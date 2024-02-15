namespace BussinessObject.DTO
{
    public class CustomerProfileGetJobDTO
    {

        public int Cid { get; set; }
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Descrition { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public bool Gender { get; set; }
        public double? Voting { get; set; }
        public int? numberJobs { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<SkillDTO>? Skills { get; set; }

        public List<int> ListSkills { get; set; }
    }
}
