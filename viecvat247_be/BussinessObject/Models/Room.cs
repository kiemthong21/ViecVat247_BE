using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? RoomName { get; set; }
        public int? NumberCustommer { get; set; }
        public virtual ICollection<CustomerRoom>? CustomerRooms { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
