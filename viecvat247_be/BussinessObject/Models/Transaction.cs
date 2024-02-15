using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? CustomerId { get; set; }
        public int? ReceiverId { get; set; }
        public int? JobId { get; set; }
        public long? Epoint { get; set; }
        public long? OldBalance { get; set; }
        public long? NewBalance { get; set; }

        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Detail { get; set; }
        public DateTime Paymentdate { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Note { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public int? TransactionType { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? BankCode { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? BankTranNo { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? CardType { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        //ma giao dich tai VNPay
        public string? TransactionNo { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? TransactionStatus { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        //ma giao dich thah toan
        public string? TxnRef { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? SecureHash { get; set; }
        
        public int? Status { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Customer? Receiver { get; set; }
        public virtual Job? Job { get; set; }
    }
}
