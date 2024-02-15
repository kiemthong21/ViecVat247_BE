using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface IJobService
    {
        //List<JobDetailDTO> GetAllJob();
        PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string typesJobs);
        PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string orderBy);
        PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string typeJob , string orderBy, string status);
        PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, int customerId,string typesJobs, string status);
        PaginatedList<Job> GetAllJob(string uid, string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string status);
        Job CreateJob(Job jobCreate);
        Job getJobById(int jid);
        Job GetJob(int id);
        void UpdateJob(Job job);

        void SendMail(string email, string subject, string message);
        void CreateJobSkill(string[]? listSkill, int jobsId);
        void DeleteJobSkillbyJobId(int jobsId);

        public void CensorshipJob(Job job, int uid, CensorshipDTO cen);


    }
}
