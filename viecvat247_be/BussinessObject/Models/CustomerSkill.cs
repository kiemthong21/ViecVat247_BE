namespace BussinessObject.Models
{
    public partial class CustomerSkill
    {

        public int CustomerSkillId { get; set; }
        public int CustomerId { get; set; }
        public int SkillId { get; set; }
        public int? Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
