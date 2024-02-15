namespace BussinessObject.Models
{
    public class CustomerRoom
    {
        public int CustomerRoomId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;

    }
}
