namespace BussinessObject.DTO
{
    public class JobUpdateDTO
    {
        public int JobsId { get; set; }
        public int? JobCategoryId { get; set; }
        public string? Title { get; set; }
        public string? Job_Overview { get; set; }
        public string? Required_Skills { get; set; }
        public string? Preferred_Skills { get; set; }
        public string? NoticeToJobSeeker { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public double? WorkingTime { get; set; }
        public double? Money { get; set; }
        public string? TypeJobs { get; set; }
        public int? NumberPerson { get; set; }
        public string[]? ListSkill { get; set; }
        public int? Status { get; set; }
    }
}
