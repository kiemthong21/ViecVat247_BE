using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ControllerDAO
{
    public class JobDAO
    {
        public static Job CreateJob(Job jobCreate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Jobs.Add(jobCreate);
                    context.SaveChanges();
                    return jobCreate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string typesJobs)
        {
            var Jobs = new List<Job>();
            int count = 0;
            IQueryable<Job> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs
                        .Include(j => j.JobAssigner)
                        .Include(j => j.JobCategory);

                    if (!string.IsNullOrWhiteSpace(cid))
                    {
                        query = query.Where(e => e.JobCategoryId.ToString().Equals(cid));

                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobsId.ToString().Equals(searchValue)
                        || e.Title.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobsId);
                                break;
                            case "title":
                                query = query.OrderBy(e => e.Title);
                                break;
                            case "title desc":
                                query = query.OrderByDescending(e => e.Title);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobsId);
                                break;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(typesJobs))
                    {
                        query = query.Where(e => e.TypeJobs.Equals(typesJobs));
                    }
                    query = query.Where(e => e.Status == 1);
                    count = query.Count();
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Jobs = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Job>(Jobs, count, pageIndex, pageSize);

        }

        public static PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var Jobs = new List<Job>();
            int count = 0;
            IQueryable<Job> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs
                        .Include(j => j.JobAssigner)
                        .Include(j => j.JobCategory);

                    if (!string.IsNullOrWhiteSpace(cid))
                    {
                        query = query.Where(e => e.JobCategoryId.ToString().Equals(cid));

                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobsId.ToString().Equals(searchValue)
                        || e.Title.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobsId);
                                break;
                            case "title":
                                query = query.OrderBy(e => e.Title);
                                break;
                            case "title desc":
                                query = query.OrderByDescending(e => e.Title);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobsId);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    Jobs = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Job>(Jobs, count, pageIndex, pageSize);

        }

        public static PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string typeJob,string orderBy, string status)
        {
            var Jobs = new List<Job>();
            int count = 0;
            IQueryable<Job> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs
                        .Include(j => j.JobAssigner)
                        .Include(j => j.JobCategory);

                    if (!string.IsNullOrWhiteSpace(cid))
                    {
                        query = query.Where(e => e.JobCategoryId.ToString().Equals(cid));

                    }
                    if (!string.IsNullOrWhiteSpace(typeJob))
                    {
                        query = query.Where(e => e.TypeJobs.Equals(typeJob));

                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobsId.ToString().Equals(searchValue)
                        || e.JobAssigner.FullName.Contains(searchValue)
                        || e.Title.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobsId);
                                break;
                            case "title":
                                query = query.OrderBy(e => e.Title);
                                break;
                            case "title desc":
                                query = query.OrderByDescending(e => e.Title);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobsId);
                                break;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    Jobs = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Job>(Jobs, count, pageIndex, pageSize);
        }

        public static PaginatedList<Job> GetAllJob(string assignerId, string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string? status)
        {
            var Jobs = new List<Job>();
            int count = 0;
            IQueryable<Job> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs
                        .Include(j => j.JobAssigner)
                        .Include(j => j.JobCategory);
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));

                    }
                    if (!string.IsNullOrWhiteSpace(assignerId))
                    {
                        query = query.Where(e => e.JobAssignerId.ToString().Equals(assignerId));

                    }
                    if (!string.IsNullOrWhiteSpace(cid))
                    {
                        query = query.Where(e => e.JobCategoryId.ToString().Equals(cid));
                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobsId.ToString().Equals(searchValue)
                        || e.Title.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobsId);
                                break;
                            case "title":
                                query = query.OrderBy(e => e.Title);
                                break;
                            case "title desc":
                                query = query.OrderByDescending(e => e.Title);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobsId);
                                break;
                        }
                    }
                    query = query.Where(e => e.Status == 1);
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    Jobs = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Job>(Jobs, count, pageIndex, pageSize);

        }
        public static PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, int customerId, string typesJobs, string status)
        {
            var Jobs = new List<Job>();
            int count = 0;
            IQueryable<Job> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs
                        .Include(j => j.JobAssigner)
                        .Include(j => j.JobCategory);

                    if (!string.IsNullOrWhiteSpace(cid))
                    {
                        query = query.Where(e => e.JobCategoryId.ToString().Equals(cid));

                    }
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobsId.ToString().Equals(searchValue)
                        || e.Title.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobsId);
                                break;
                            case "title":
                                query = query.OrderBy(e => e.Title);
                                break;
                            case "title desc":
                                query = query.OrderByDescending(e => e.Title);
                                break;
                            case "address":
                                query = query.OrderBy(e => e.Address);
                                break;
                            case "address desc":
                                query = query.OrderByDescending(e => e.Address);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobsId);
                                break;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(typesJobs))
                    {
                        query = query.Where(e => e.TypeJobs.Equals(typesJobs));
                    }
                    query = query.Where(e => e.JobAssignerId == customerId);
                    count = query.Count();
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Jobs = query.ToList();
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Job>(Jobs, count, pageIndex, pageSize);
        }

        public static Job GetJobById(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    Job jobs = context.Jobs
                         .Include(j => j.JobAssigner)
                         .Include(j => j.JobCategory)
                         .SingleOrDefault(x => x.JobsId == id);
                    return jobs;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateJob(Job job)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Job>(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void CreateJobSkill(string[]? listSkill, int jobsId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    if (listSkill == null || listSkill.Length == 0)
                    {
                        return;
                    }
                    foreach (string skill in listSkill)
                    {
                        JobsSkill jk = new JobsSkill();
                        jk.SkillId = int.Parse(skill);
                        jk.JobId = jobsId;
                        jk.Status = 1;
                        context.JobsSkills.Add(jk);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteJobSkillbyJobId(int jobsId)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var jobSkillsToDelete = context.JobsSkills.Where(js => js.JobId == jobsId).ToList();
                    context.JobsSkills.RemoveRange(jobSkillsToDelete);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static void CensorshipJob(Job job, int uid, CensorshipDTO cen)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Job>(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    ReasonForEditingJobStatus reason = new ReasonForEditingJobStatus();
                    reason.UserId = uid;
                    reason.JobId = job.JobsId;
                    reason.Note = cen.Note;
                    reason.Status = cen.Status;
                    reason.Timestamp = DateTime.UtcNow;        
                    var r = context.ReasonForEditingJobStatus.Where(r => r.JobId == job.JobsId).Count();
                    reason.NumberProcessingTime = r + 1;
                    context.ReasonForEditingJobStatus.Add(reason);
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
