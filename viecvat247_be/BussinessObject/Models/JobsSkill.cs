namespace BussinessObject.Models
{
    public partial class JobsSkill
    {
        public int JobsSkillId { get; set; }
        public int SkillId { get; set; }
        public int JobId { get; set; }
        public int? Status { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
