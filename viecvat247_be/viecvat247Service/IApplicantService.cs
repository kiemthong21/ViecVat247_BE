using BusinessObject;
using BussinessObject.Models;

namespace viecvat247Service
{
    public interface IApplicantService
    {
        PaginatedList<Application> GetApplications(string status, string typejob, string search, string jobCategory ,string order, int pageIndex, int pageSize, int appID);

        void DeleteApplication(Application application);

        Application GetApplication(int id);

        void UpdateApplication(Application application);

        Application CreateApplication(Application application);

        void SendMailSetDone();

        void SendMailAppySuccessfull(string gmail, string jobid, string jobName);

        void SendMailApply(Job job);

        void SendMailCancelJobAssigner(string gmail, string jobName);

        int CountApplySuccessByJob(int jobId);

        void RejectAllApplicationPendingByJob(int jobId);

        Application CheckApplication(int jobId, int cusId);

        PaginatedList<Application> GetCustomersApplyByJob(int jobid, string searchValue, int pageIndex, int pageSize, string orderBy, string? status);
        int getNumberSeekerApplyByJobId(int jobId);
        int getNumberSeekerApplyByStatus(int jobId, int status);
        PaginatedList<Application> GetReportsByCid(int cid, int pageIndex, int pageSize);
        int GetNumberFeedbackByCid(int cid);

        void DeleteAppication(Application application);
    }
}
