using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Application
    {
        public int Aid { get; set; }
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Feedback { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RateDate { get; set; }
        public double? Money { get; set; }
        public double? Voting { get; set; }
        public int? Status { get; set; }

        public virtual Customer Applicant { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
    }
}
