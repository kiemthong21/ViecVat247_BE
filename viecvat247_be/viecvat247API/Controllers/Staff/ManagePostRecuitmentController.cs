using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using viecvat247API.Hubs;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Staff
{
    [Route("api/Staff")]
    [ApiController]
    public class ManagePostRecuitmentController : ControllerBase
    {

        private IJobService _jobService = new JobService();
        private INotificationService _notificationService = new NotificationService();

        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;

        public ManagePostRecuitmentController(IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet("ListAllJobs")]
        public IActionResult getAllJob([FromQuery] string? cid, string? searchValue, string? typeJob , string? orderBy, string? status,
            int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            PaginatedList<Job> jobs = _jobService.GetAllJobStaff(cid, searchValue, pageIndex, pageSize, typeJob, orderBy, status);
            return Ok(new { totalItems = jobs.Totalsize, totalPage = jobs.TotalPages, Jobs = _mapper.Map<List<JobDetailDTO>>(jobs) });
        }

        [Authorize]
        [HttpPost("Censorship/{jobid}")]
        public async Task<IActionResult> Censorship(int jobid, CensorshipDTO cen)
        {
            var job = _jobService.GetJob(jobid);
            if (job == null)
            {
                return Ok(new { message = "not_found" }); ;
            }
            else
            {
                //approve
                var userId = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                job.Status = cen.Status;
                // Đăng tin
                if (cen.Status == 1)
                {
                    _jobService.CensorshipJob(job, Int32.Parse(userId), cen);
                    string content = "Công việc " + job.Title + " đã được đăng thành công.";
                    var notification = _notificationService.AddNotification(content, job?.JobAssignerId.ToString(), jobid.ToString(), null);
                    await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                    _jobService.SendMail(job.JobAssigner.Cemail, "Tin tuyển dụng đã được đăng thành công.", "Tin tuyển dụng của bạn đã được đăng thành công. Hãy đăng nhập vào hệ thống để check");
                }
                //Yêu cầu chỉnh sửa
                else if (cen.Status == 2)
                {
                    _jobService.CensorshipJob(job, Int32.Parse(userId), cen);
                    string content = "Công việc " + job.Title + " đã bị đưa về trạng thái chỉnh sửa do thiếu thông tin.";
                    var notification = _notificationService.AddNotification(content, job?.JobAssignerId.ToString(), jobid.ToString(), null);
                    await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                    _jobService.SendMail(job.JobAssigner.Cemail, "Tin tuyển dụng của bạn đã bị thiếu hoặc sai thông tin.", "Tin tuyển dụng của bạn đã bị thiếu hoặc sai thông tin. Bài viết đã vi phạm 1 số quy định của  của hệ thống vui lòng hãy chỉnh sửa lại nội dung bài đăng " + cen?.Note);
                }


                //reject luon
                else if (cen.Status == 3)
                {
                    _jobService.CensorshipJob(job, Int32.Parse(userId), cen);
                    string content = "Công việc " + job.Title + " đã bị hủy do vi phạm 1 số nguyên tắc của nền tảng.";
                    var notification = _notificationService.AddNotification(content, job?.JobAssignerId.ToString(), jobid.ToString(), null);
                    await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                    _jobService.SendMail(job.JobAssigner.Cemail, "Tin tuyển dụng của bạn đã bị reject.", "Tin tuyển dụng của bạn đã bị reject. Bài viết đã vi phạm 1 số điều luật của hệ thống " + cen?.Note);
                }
                else
                {
                    return BadRequest(new { error = "error" });
                }
                _jobService.UpdateJob(job);
            }

            return Ok(new { message = "check_mail" });
        }

    }
}
