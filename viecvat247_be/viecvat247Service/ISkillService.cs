using BusinessObject;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface ISkillService
    {
        PaginatedList<Skill> GetAllSkills(string searchValue, string cate, int pageIndex, int pageSize, string orderBy);

        PaginatedList<SkillCategory> GetSkillCategories(string searchValue, int pageIndex, int pageSize, string orderBy);

        Skill AddNewSkill(Skill skill);

        SkillCategory AddNewSkillCategory(SkillCategory cate);

        void UpdateSkill(Skill skill);

        void UpdateSkillCatgory(SkillCategory cate);

        Skill GetSkill(int id);

        Skill GetSkill(string name);

        SkillCategory GetSkillCategory(string name);

        SkillCategory GetSkillCategory(int id);

        void DeleteSkill(Skill skill);

        void DeleteSkillCategory(SkillCategory category);
        List<Skill> GetAllSkillByJobId(int jid);

    }
}
