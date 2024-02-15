using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class User
    {
        public User()
        {
            ResionEditStatusJobs = new HashSet<ReasonForEditingJobStatus>();
        }

        public int Uid { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Uemail { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Username { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Password { get; set; }
        public int? Role { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        [MaxLength(13)]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Address { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? FullName { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime? Dob { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<TypeManagerUser> TypeManagerUsers { get; set; }
        public virtual ICollection<ReasonForEditingJobStatus> ResionEditStatusJobs { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

    }
}
