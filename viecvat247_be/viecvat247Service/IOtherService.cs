using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface IOtherService
    {
        string Encrypt(string toEncrypt);

        string Decrypt(string toDecrypt);

        string GenerateRandomString(int length);

        PaginatedList<Skill> GetAllSkills(string searchValue, string cate, string orderBy);

        PaginatedList<SkillCategory> GetSkillCategories(string searchValue, string orderBy);

        PaginatedList<JobsCategory> GetJobCategory(string searchValue, string orderBy);
        DashboardDTO GetStatisticsDaskboard(int year);
        List<ChartDTO> GetChart(int year);
    }
}
