namespace BussinessObject.DTO
{
    public class FeedbackDTO
    {
        public int JobId { get; set; }
        public string? JobName { get; set; }
        public int JobAssignerId { get; set; }
        public string? JobAssignerName { get; set; }
        public string? Feedback { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Money { get; set; }
        public double? Voting { get; set; }
        public string[] SkillName { get; set; }
    }
}
