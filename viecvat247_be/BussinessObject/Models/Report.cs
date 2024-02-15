using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int? Cid { get; set; }
        public int? Uid { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Content { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Note { get; set; }
        public DateTime? Timestamp { get; set; }
        public DateTime? TimeFeedback { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Feedback { get; set; }
        public int? Status { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ReportImage>? ReportImages { get; set; }

    }
}
