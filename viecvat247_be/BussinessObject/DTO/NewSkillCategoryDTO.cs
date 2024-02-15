using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class NewSkillCategoryDTO
    {
        [Required(ErrorMessage = "Category Name is require")]
        public string? SkillCategoryName { get; set; }

        [Required(ErrorMessage = "Category Description is require")]
        public string? Description { get; set; }
    }
}
