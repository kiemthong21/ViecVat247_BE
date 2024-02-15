using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? CustomerId { get; set; }
        public int? JobId { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Description { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? Status { get; set; }

        public int? Aid { get; set; } = null;
        public virtual Customer? Customer { get; set; }
        public virtual Job? Job { get; set; }
    }
}
