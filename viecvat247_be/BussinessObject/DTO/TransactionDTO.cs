namespace BussinessObject.DTO
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? ReceiverId { get; set; }
        public string? ReceiverName { get; set; }
        public int? JobId { get; set; }
        public string? JobName { get; set; }
        public long? Epoint { get; set; }
        public string? Detail { get; set; }
        public DateTime Paymentdate { get; set; }
        public string? Note { get; set; }
        public int? TransactionType { get; set; }
        public string? BankCode { get; set; }
        public long? OldBalance { get; set; }
        public long? NewBalance { get; set; }
        public int? Status { get; set; }
    }
}
