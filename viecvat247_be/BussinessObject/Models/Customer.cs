using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Customer
    {

        public int Cid { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Cemail { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Password { get; set; }
        public int? Role { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        [MaxLength(13)]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? FullName { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Location { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Address { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Descrition { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? Epoint { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? CV { get; set; }
        public bool Gender { get; set; }
        public double? Voting { get; set; }
        public int? Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? VerifyCode { get; set; }
        public int? FrofileStatus { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Application>? Applications { get; set; }
        public virtual ICollection<CustomerSkill>? CustomerSkills { get; set; }
        public virtual ICollection<CustomerRoom>? CustomerRooms { get; set; }
        public virtual ICollection<Job>? Jobs { get; set; }
        public virtual ICollection<Message>? MessageSenders { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
