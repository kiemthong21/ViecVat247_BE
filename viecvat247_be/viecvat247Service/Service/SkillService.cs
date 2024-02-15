using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;

namespace viecvat247Service.Service
{
    public class SkillService : ISkillService
    {
        public Skill AddNewSkill(Skill skill)
        => SkillDAO.AddSkill(skill);

        public SkillCategory AddNewSkillCategory(SkillCategory cate)
        => SkillDAO.AddSkillCategory(cate);

        public void DeleteSkill(Skill skill)
        => SkillDAO.DeleteSkill(skill);

        public void DeleteSkillCategory(SkillCategory category)
        => SkillDAO.DeleteSkillCategory(category);

        public List<Skill> GetAllSkillByJobId(int jid)
        => SkillDAO.GetAllSkillsByJobId(jid);

        public PaginatedList<Skill> GetAllSkills(string searchValue, string cate, int pageIndex, int pageSize, string orderBy)
        => SkillDAO.GetAllSkills(searchValue, cate, pageIndex, pageSize, orderBy);

        public Skill GetSkill(int id)
        => SkillDAO.GetSkillById(id);

        public Skill GetSkill(string name)
        => SkillDAO.GetSkillByName(name);

        public PaginatedList<SkillCategory> GetSkillCategories(string searchValue, int pageIndex, int pageSize, string orderBy)
        => SkillDAO.GetAllSkillCategory(searchValue, pageIndex, pageSize, orderBy);

        public SkillCategory GetSkillCategory(int id)
        => SkillDAO.GetSkillCategoryById(id);

        public SkillCategory GetSkillCategory(string name)
        => SkillDAO.GetSkillCategoryByName(name);

        public void UpdateSkill(Skill skill)
        => SkillDAO.UpdateSkill(skill);

        public void UpdateSkillCatgory(SkillCategory cate)
        => SkillDAO.UpdateSkillCategory(cate);
    }
}
