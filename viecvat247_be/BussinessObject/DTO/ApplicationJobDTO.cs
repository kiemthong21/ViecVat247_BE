using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class ApplicationJobDTO
    {
        public int Aid { get; set; }
        public int JobsId { get; set; }
        public int? JobAssignerId { get; set; }
        public int? JobCategoryId { get; set; }

        public string? JobCategory { get; set; }

        public string? Title { get; set; }

        public string? Image { get; set; }

        public string? Job_Overview { get; set; }

        public string? Required_Skills { get; set; }

        public string? Preferred_Skills { get; set; }

        public string? NoticeToJobSeeker { get; set; }

        public string? Location { get; set; }

        public string? Address { get; set; }
        public double? WorkingTime { get; set; }
        public double? Money { get; set; }
        public int? NumberPerson { get; set; }

        public string? TypeJobs { get; set; }
        public int? JobStatus { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? JobAssignerEmail { get; set; }

        public int ApplicantID { get; set; }

        public int? ApplyStatus { get; set; }
    }
}
