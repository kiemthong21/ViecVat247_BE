using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;

namespace viecvat247Service.Service
{
    public class JobCategoryService : IJobCategoryService
    {
        public JobsCategory AddNewJobsCategory(JobsCategory jobs)
          => CategoryJobsDAO.AddCategoryJob(jobs);

        public PaginatedList<JobsCategory> GetJobsCategories(string searchValue, int pageIndex, int pageSize, string orderBy)
        => CategoryJobsDAO.GetAllJobCategory(searchValue, pageIndex, pageSize, orderBy);

        public JobsCategory GetJobsCategory(int jobId)
        => CategoryJobsDAO.GetJobCategory(jobId);

        public JobsCategory GetJobsCategory(string jobName)
        => CategoryJobsDAO.GetJobCategory(jobName);

        public void UpdateJobCategory(JobsCategory job)
        {
            CategoryJobsDAO.UpdateJobCategory(job);
        }
    }
}
