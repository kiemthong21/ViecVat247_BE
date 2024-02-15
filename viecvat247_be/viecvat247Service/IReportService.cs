using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viecvat247Service
{
    public interface IReportService
    {
        Report GetReport(int id);

        PaginatedList<Report> GetReports(string searchValue, string status, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string orderBy);

        Report AddReport(NewReportDTO report, int cid);

        void UpdateReport(Report report);

        void DeleteReport(Report report);

        void SendFeedbackReport(Report report);
    }
}
