using BussinessObject.Models;
using BussinessObject.Viecvat247Context;

namespace DataAccess.ControllerDAO
{
    public class ManageDAO
    {
        public int TotalNumberJob(string month, string status)
        {
            int count = 0;
            IQueryable<Job> query;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Jobs;

                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(month))
                    {
                        DateTime time = DateTime.Now;
                        string startdate = "1-" + month + "-" + time.Year.ToString();
                        string enddate = "1-" + (Int32.Parse(month + 1)).ToString() + "-" + time.Year.ToString();
                        query = query.Where(e => e.StartDate >= DateTime.Parse(startdate) && e.StartDate <= DateTime.Parse(enddate));
                    }

                    count = query.Count();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public int TotalNumberApply(string month, string status)
        {
            int count = 0;
            IQueryable<Application> query;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Applications;

                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }
                    if (!string.IsNullOrWhiteSpace(month))
                    {
                        DateTime time = DateTime.Now;
                        string startdate = "1-" + month + "-" + time.Year.ToString();
                        string enddate = "1-" + (Int32.Parse(month + 1)).ToString() + "-" + time.Year.ToString();
                        query = query.Where(e => e.StartDate >= DateTime.Parse(startdate) && e.StartDate <= DateTime.Parse(enddate));
                    }

                    count = query.Count();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public int TotalUser(string month)
        {
            int count = 0;
            IQueryable<Customer> query;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Customers;
                    if (!string.IsNullOrWhiteSpace(month))
                    {
                        DateTime time = DateTime.Now;
                        string startdate = "1-" + month + "-" + time.Year.ToString();
                        string enddate = "1-" + (Int32.Parse(month + 1)).ToString() + "-" + time.Year.ToString();
                        query = query.Where(e => e.CreateDate >= DateTime.Parse(startdate) && e.CreateDate <= DateTime.Parse(enddate));
                    }

                    count = query.Count();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

    }
}
