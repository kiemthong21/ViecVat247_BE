using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CustomerSkills = new HashSet<CustomerSkill>();
            JobsSkills = new HashSet<JobsSkill>();
        }
        public int SkillId { get; set; }
        public int? SkillCategoryId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? SkillName { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Description { get; set; }
        public string? TypeSkill { get; set; }
        public int? Status { get; set; }

        public virtual SkillCategory? SkillCategory { get; set; }
        public virtual ICollection<CustomerSkill> CustomerSkills { get; set; }
        public virtual ICollection<JobsSkill> JobsSkills { get; set; }
    }
}
