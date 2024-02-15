namespace BussinessObject.DTO
{
    public class SkillDTO
    {
        public int SkillId { get; set; }
        public int? SkillCategoryId { get; set; }
        public string? SkillCategoryName { get; set; }
        public string? SkillName { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
    }
}
