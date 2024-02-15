using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ControllerDAO
{
    public class ApplicationDAO
    {
        public static Application AddNewApplication(Application app)
        {
            try
            {
                using var context = new Viecvat247DBcontext();
                context.Applications.Add(app);
                context.SaveChanges();
                return app;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Application GetApplication(int jobId, int cusId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    return context.Applications.Include(x => x.Job)
                        .ThenInclude(x => x.JobCategory).SingleOrDefault(x => x.JobId == jobId && x.ApplicantId == cusId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int CountAllApplicationsSuccessByJob(int jobId)
        {

            var count = 0;
            try
            {
                using var context = new Viecvat247DBcontext();
                count = context.Applications.Count(x => x.JobId == jobId && (x.Status == 1 || x.Status == 2));


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }



        public static void DeletableApplication(Application app)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Applications.Remove(app);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Application GetApplication(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    return context.Applications.Include(x => x.Applicant).Include(x => x.Job.JobAssigner).Include(x => x.Job).ThenInclude(x => x.JobCategory).SingleOrDefault(x => x.Aid == id) ?? null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateApplication(Application app)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Application>(app).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    if (app.Status == 2 && app.Voting != null)
                    {
                        var customer = context.Customers.SingleOrDefault(x => x.Cid == app.ApplicantId);
                        List<Application> listRate = context.Applications.Where(x => x.ApplicantId == app.ApplicantId && x.Status == 2 && x.Voting != null).ToList();
                        double? rate = listRate.Average(x => x.Voting);
                        customer.Voting = Math.Round((double)rate, 1);
                        context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static void RejectAllApplicationPendingByJob(int jobId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var listApply = context.Applications.Where(x => x.JobId == jobId && x.Status == 0);
                    if (listApply == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var applicant in listApply)
                        {
                            applicant.Status = 3;
                            context.Entry<Application>(applicant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //public static PaginatedList<Application> GetApplications(string status, int pageIndex, int pageSize, int appID)
        //{
        //    var applicant = new List<Application>();
        //    int count = 0;
        //    IQueryable<Application> query = null;

        //    try
        //    {
        //        using (var context = new Viecvat247DBcontext())
        //        {
        //            query = context.Applications.Include(x => x.Job).ThenInclude(x => x.JobCategory).Where(x => x.ApplicantId == appID);
        //            count = query.Count();
        //            if (!string.IsNullOrWhiteSpace(status))
        //            {
        //                query = query.Where(e => e.Status.ToString().Equals(status));
        //            }

        //            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //            applicant = query.ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return new PaginatedList<Application>(applicant, count, pageIndex, pageSize);
        //}

        public static PaginatedList<Application> GetApplications(string status, string typejob,
            string searchValue,string jobCategory , string orderBy, int pageIndex, int pageSize, int appID)
        {
            var applicant = new List<Application>();
            int count = 0;
            IQueryable<Application>? query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Applications.Include(x => x.Job).ThenInclude(x => x.JobCategory).Where(x => x.ApplicantId == appID);
                    if (!string.IsNullOrWhiteSpace(typejob))
                    {
                        query = query.Where(e => e.Job.TypeJobs.ToLower().Equals(typejob.ToLower()));

                    }
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e =>  e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.Job.JobsId.ToString().Equals(searchValue)
                        || e.Job.Title.Contains(searchValue)
                        || e.Job.Address.Contains(searchValue));
                    }
                    if (!string.IsNullOrWhiteSpace(jobCategory))
                    {
                        query = query.Where(e => e.Job.JobCategoryId.ToString().Equals(jobCategory));
                    }
                    if (query != null) // Kiểm tra xem query có phải là null hay không
                    {
                        count = query.Count();
                        if (!string.IsNullOrWhiteSpace(orderBy))
                        {
                            switch (orderBy)
                            {
                                case "id desc":
                                    query = query.OrderByDescending(e => e.Job.JobsId);
                                    break;
                                case "title":
                                    query = query.OrderBy(e => e.Job.Title);
                                    break;
                                case "title desc":
                                    query = query.OrderByDescending(e => e.Job.Title);
                                    break;
                                case "address":
                                    query = query.OrderBy(e => e.Job.Address);
                                    break;
                                case "address desc":
                                    query = query.OrderByDescending(e => e.Job.Address);
                                    break;
                                default:
                                    query = query.OrderBy(e => e.Job.JobsId);
                                    break;
                            }
                        }
                        

                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                        applicant = query.ToList();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Application>(applicant, count, pageIndex, pageSize);
        }


        public static PaginatedList<Application> GetCustomersApplyByJob(int jobid, string searchValue, int pageIndex, int pageSize, string orderBy, string? status)
        {
            var customer = new List<Application>();
            int count = 0;
            IQueryable<Application> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Applications.Include(x => x.Applicant).
                        Where(c => c.JobId == jobid);
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.ApplicantId.ToString().Equals(searchValue)
                        || e.Applicant.Cemail.Contains(searchValue)
                        || e.Applicant.FullName.Contains(searchValue)
                        || e.Applicant.PhoneNumber.Contains(searchValue)
                        || e.Applicant.Address.Contains(searchValue));
                    }
                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "date_desc":
                                query = query.OrderBy(c => c.ApplicationDate);
                                break;
                            case "vote":
                                query = query.OrderBy(e => e.Applicant.Voting);
                                break;
                            case "vote_desc":
                                query = query.OrderByDescending(e => e.Applicant.Voting);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.Applicant.FullName);
                                break;
                            case "name_desc":
                                query = query.OrderByDescending(e => e.Applicant.FullName);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Applicant.Address);
                                break;
                            case "address_desc":
                                query = query.OrderByDescending(e => e.Applicant.Address);
                                break;
                            default:
                                query = query.OrderBy(c => c.ApplicationDate);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    customer = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Application>(customer, count, pageIndex, pageSize);
        }

        public static int GetNumberFeedbackByCid(int cid)
        {
            int count = 0;
            IQueryable<Application> query = null;
            var customer = new List<Application>();
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Applications
                        .Where(app => app.ApplicantId == cid && app.Voting != null && app.Feedback != null && app.Status == 2);
                    count = query.Count();
                    customer= query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public static PaginatedList<Application> GetReportsByCid(int cid, int pageIndex, int pageSize)
        {
            var Applications = new List<Application>();
            int count = 0;
            IQueryable<Application> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Applications
                        .Where(app => app.ApplicantId == cid && app.Voting != null && app.Feedback != null && app.Status ==2 )
                        .Include(app => app.Job)
                        .ThenInclude(job => job.JobsSkills)
                        .ThenInclude(jobsSkill => jobsSkill.Skill)
                        .Include(app => app.Job.JobAssigner);
                    count = query.Count();
                    query = query.OrderByDescending(x => x.EndDate);
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Applications = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Application>(Applications, count, pageIndex, pageSize);
        }

        public static int getNumberSeekerApplyByStatus(int jobId, int status)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    int totalCount = context.Applications
                    .Where(app => app.JobId == jobId && app.Status == status)
                    .Count();

                    return totalCount;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int getNumberSeekerApplyByJobId(int jobId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    int totalCount = context.Applications
                    .Where(app => app.JobId == jobId && (app.Status == 1  || app.Status == 2))
                    .Count();

                    return totalCount;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void deleteApplication(Application a)
        {

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                   context.Applications.Remove(a);
                   context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
