namespace BussinessObject.DTO
{
    public class JobDetailDTO
    {
        public int JobsId { get; set; }
        public int? JobAssignerId { get; set; }
        public string? JobAssignerName { get; set; }
        public int? JobCategoryId { get; set; }
        public string? JobCategoryName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Job_Overview { get; set; }
        public string? Required_Skills { get; set; }
        public string? Preferred_Skills { get; set; }
        public string? NoticeToJobSeeker { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? WorkingTime { get; set; }
        public double? Money { get; set; }
        public int? NumberPerson { get; set; }
        public string? TypeJobs { get; set; }
        public List<int> Skills { get; set; }
        public int? SkillCategoryId { get; set; }
        public int? Status { get; set; }
    }
}
