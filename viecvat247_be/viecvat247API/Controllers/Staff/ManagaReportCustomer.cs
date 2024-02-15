using AutoMapper;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using viecvat247Service.Service;
using viecvat247Service;
using BusinessObject;
using MailKit.Search;
using Microsoft.OData.Edm;

namespace viecvat247API.Controllers.Staff
{
    [Route("api/Staff")]
    [ApiController]
    public class ManagaReportCustomerController : ControllerBase
    {
        private IReportService _reportService = new ReportService();
        private IStaffService _staffService = new StaffService();
        private readonly IMapper _mapper;
        public ManagaReportCustomerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetReports")]
        public IActionResult GetReports([FromQuery] string? searchValue, string? status, DateTime? startDate, DateTime? endDate,
            string? orderBy, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    BadRequest(new { error = "error" });
                }
                PaginatedList<Report> reports = _reportService.GetReports(searchValue, status, startDate, endDate, pageIndex, pageSize, orderBy);
                var reportDTO = reports.Select(e => _mapper.Map<ReportDTO>(e));
                return Ok(new { totalItems = reports.Totalsize, totalPages = reports.TotalPages, Reports = reportDTO });

            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }



        [Authorize]
        [HttpGet("Report/{id}")]
        public IActionResult GetReport(int id)
        {
            try
            {
                Report report = _reportService.GetReport(id);
                if (report == null)
                {
                    return Ok(new { message = "not_found" });
                }
                return Ok(_mapper.Map<ReportDTO>(report));
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [AllowAnonymous]
        [HttpPut("SendFeedbackReport/{id}")]
        public IActionResult SendFeedbackReport(int id, ReportFeedbackDTO reportFeedbackDTO)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (_staffService.GetStaffById(Int32.Parse(uid)) == null)
                {
                    return BadRequest(new { error = "error" });
                }
                Report report = _reportService.GetReport(id);
                if (report == null)
                {
                    return Ok(new { message = "not_found" });
                }
                report.TimeFeedback = DateTime.Now;
                report.Feedback = reportFeedbackDTO.Feedback;
                report.Status = 1;
                report.Uid = Int32.Parse(uid);
                _reportService.UpdateReport(report);
                _reportService.SendFeedbackReport(report);
                return Ok(new { message = "feedback_done" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }
    }
}
