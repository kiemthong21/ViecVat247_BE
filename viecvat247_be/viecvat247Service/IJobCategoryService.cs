using BusinessObject;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface IJobCategoryService
    {
        void UpdateJobCategory(JobsCategory job);

        JobsCategory AddNewJobsCategory(JobsCategory jobs);


        PaginatedList<JobsCategory> GetJobsCategories(string searchValue, int pageIndex, int pageSize, string orderBy);

        JobsCategory GetJobsCategory(int jobId);

        JobsCategory GetJobsCategory(string jobName);
    }
}
