using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class JobsCategory
    {
        public JobsCategory()
        {
            Jobs = new HashSet<Job>();
        }

        public int JobCategoryId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? JobCategoryName { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Description { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
