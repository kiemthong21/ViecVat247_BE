namespace BussinessObject.DTO
{
    public class NewApplyRequestDTO
    {
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Money { get; set; }
        public double? Voting { get; set; }
        public int? Status { get; set; }
    }
}
