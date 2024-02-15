

namespace BussinessObject.DTO
{
    public class ReportDTO
    {
        public int ReportId { get; set; }
        public int? Cid { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public int? Uid { get; set; }

        public string? EmployeeName { get; set; }
     
        public string? Content { get; set; }
        public string? Note { get; set; }
        public DateTime? Timestamp { get; set; }
        public DateTime? TimeFeedback { get; set; }
        public string? Feedback { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<ReportImageDTO>? ReportImages { get; set; }
    }
}
