using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class SkillCategory
    {
        public SkillCategory()
        {
            Skills = new HashSet<Skill>();
        }

        public int SkillCategoryId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? SkillCategoryName { get; set; }
        [Column(TypeName = "nvarchar(1255)")]
        [MaxLength(1255)]
        public string? Description { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
