using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class NewSkillDTO
    {
        [Required(ErrorMessage = "Skill Category is require")]
        public int SkillCategoryId { get; set; }

        [Required(ErrorMessage = "Skill Name is require"), MaxLength(50)]
        public string? SkillName { get; set; }

        [Required(ErrorMessage = "Skill Description is require"), MaxLength(300)]
        public string? Description { get; set; }
    }
}
