using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class NewCategoryJobDTO
    {
        [Required(ErrorMessage = "Category Job Name is require")]
        public string? JobCategoryName { get; set; }

        [Required(ErrorMessage = "Category Job Description is require")]
        public string? Description { get; set; }
    }
}
