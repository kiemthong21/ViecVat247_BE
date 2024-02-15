using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ControllerDAO
{
    public class ReportDAO
    {
        public static Report GetReportById(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var report = context.Reports.Include(x => x.ReportImages).Include(x => x.Customer).Include(x => x.User).SingleOrDefault(x => x.ReportId == id);
                    if (report != null)
                    {
                        return report;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<Report> GetReports(string searchValue, string status, DateTime? startDate, DateTime? endDate ,int pageIndex, int pageSize, string orderBy)
        {
            var reports = new List<Report>();
            int count = 0;
            IQueryable<Report> query;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Reports.Include(x => x.ReportImages).Include(x => x.Customer).Include(x => x.User);
                    if(!string.IsNullOrEmpty(status))
                    {
                        query = query.Where(e => e.Status.ToString().Equals(status));
                    }

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.ReportId.ToString().Equals(searchValue) || e.Customer.FullName.ToLower().Contains(searchValue));
                    }
                    if (startDate.HasValue)
                    {
                        query = query.Where(e => e.Timestamp >= startDate);
                    }
                    if(endDate.HasValue)
                    {
                        query = query.Where(e => e.Timestamp <= endDate);
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id_desc":
                                query = query.OrderByDescending(e => e.ReportId);
                                break;
                            case "customer":
                                query = query.OrderBy(e => e.Customer.FullName);
                                break;
                            case "customer_desc":
                                query = query.OrderByDescending(e => e.Customer.FullName);
                                break;
                            default:
                                query = query.OrderBy(e => e.ReportId);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    reports = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Report>(reports, count, pageIndex, pageSize);
        }

        public static Report AddReport(NewReportDTO reportDTO, int cid)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    Report report = new Report();
                    report.Cid = cid;
                    report.Timestamp = DateTime.Now;
                    report.Content = reportDTO.Content;
                    report.Status = 0;
                    context.Reports.Add(report);
                    context.SaveChanges();
                    foreach (string item in reportDTO.ImageReport)
                    {
                        ReportImage image = new ReportImage();
                        image.Image = item;
                        image.ReportId = report.ReportId;
                        image.ImageType = 1;
                        context.ReportImages.Add(image);
                    }
                    context.SaveChanges();
                    return report;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static void UpdateReport(Report report)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Report>(report).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteReport(Report report)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var rp = context.Reports.SingleOrDefault(x => x.ReportId == report.ReportId);
                    if (rp != null)
                    {
                        context.Reports.Remove(rp);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
