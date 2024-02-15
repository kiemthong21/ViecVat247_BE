using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Job
    {
        public Job()
        {
            Applications = new HashSet<Application>();
            JobsSkills = new HashSet<JobsSkill>();
            Notifications = new HashSet<Notification>();
            Transactions = new HashSet<Transaction>();
        }


        public int JobsId { get; set; }
        public int? JobAssignerId { get; set; }
        public int? JobCategoryId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Title { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Image { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Job_Overview { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Required_Skills { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Preferred_Skills { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? NoticeToJobSeeker { get; set; }

        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Location { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public double? WorkingTime { get; set; }
        public double? Money { get; set; }
        public int? NumberPerson { get; set; }
        public string? TypeJobs { get; set; }
        public int? Status { get; set; }
        public bool? IsSendMail { get; set; }
        public virtual Customer? JobAssigner { get; set; }
        public virtual JobsCategory? JobCategory { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<ReasonForEditingJobStatus> ReasonForEditingJobStatus { get; set; }
        public virtual ICollection<JobsSkill> JobsSkills { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
