using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? SenderId { get; set; }

        public int? RoomId { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? MessageContent { get; set; }
        public int? MessageType { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Customer? Sender { get; set; }
        public virtual Room? Room { get; set; }
    }
}
