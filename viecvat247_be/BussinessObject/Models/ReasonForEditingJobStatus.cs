using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class ReasonForEditingJobStatus
    {
        public int Rid { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Note { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? NumberProcessingTime { get; set; }
        public int? Status { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
    }
}
