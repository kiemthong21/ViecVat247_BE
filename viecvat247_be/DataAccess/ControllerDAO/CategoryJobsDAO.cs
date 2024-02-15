using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;

namespace DataAccess.ControllerDAO
{
    public class CategoryJobsDAO
    {
        public static JobsCategory AddCategoryJob(JobsCategory cate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.JobsCategories.Add(cate);
                    context.SaveChanges();
                    return cate;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateJobCategory(JobsCategory cate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<JobsCategory>(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<JobsCategory> GetAllJobCategory()
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cate = context.JobsCategories.OrderBy(x => x.JobCategoryName).Where(x => x.Status == 1).ToList();
                    return cate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<JobsCategory> GetAllJobCategory(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var cate = new List<JobsCategory>();
            int count = 0;
            IQueryable<JobsCategory> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.JobsCategories;

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobCategoryName.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobCategoryId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.JobCategoryName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.JobCategoryName);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobCategoryId);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    cate = query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<JobsCategory>(cate, count, pageIndex, pageSize);
        }

        public static PaginatedList<JobsCategory> GetAllJobCategory(string searchValue, string orderBy)
        {
            var cate = new List<JobsCategory>();
            int count = 0;
            IQueryable<JobsCategory> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.JobsCategories.Where(x => x.Status == 1);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.JobCategoryName.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.JobCategoryId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.JobCategoryName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.JobCategoryName);
                                break;
                            default:
                                query = query.OrderBy(e => e.JobCategoryId);
                                break;
                        }
                    }
                    count = query.Count();
                    cate = query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<JobsCategory>(cate, count, 1, 999999);
        }
        public static JobsCategory GetJobCategory(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cate = context.JobsCategories.SingleOrDefault(x => x.JobCategoryId == id);
                    return cate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static JobsCategory GetJobCategory(string name)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cate = context.JobsCategories.SingleOrDefault(x => x.JobCategoryName.Equals(name ));
                    return cate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
