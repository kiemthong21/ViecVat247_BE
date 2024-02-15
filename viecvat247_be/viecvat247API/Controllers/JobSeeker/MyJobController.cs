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

namespace viecvat247API.Controllers.JobSeeker
{
    [Route("api/JobSeeker")]
    [ApiController]
    public class MyJobController : ControllerBase
    {
        private IApplicantService _applicantService = new ApplicantService();
        private IJobService _jobService = new JobService();
        private ICustomerService _customerService = new CustomerService();
        private INotificationService _notificationService = new NotificationService();
        private IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;
        public MyJobController(IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet("GetMyJob")]
        public IActionResult GetMyJob([FromQuery] string? status, string? typejob, string? jobCategory, string? search, string? order, int pageIndex = 1, int pageSize = 10)
        {
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            if (cid == null)
            {
                return BadRequest(new { error = "error" });
            }
            int myId = Int32.Parse(cid);
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest(new { error = "error" });
                }
                PaginatedList<Application> app = _applicantService.
                    GetApplications(status, typejob, search, jobCategory, order, pageIndex, pageSize, myId);
                var applications = app.Select(e => _mapper.Map<ApplicationJobDTO>(e));
                return Ok(new { totalItems = app.Totalsize, totalPage = app.TotalPages, Jobs = applications });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        //[Authorize]
        //[HttpDelete("RejectJob/{id}")]
        //public IActionResult RejectJob(int id)
        //{
        //    var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
        //    if (cid == null)
        //    {
        //        return BadRequest(new { error = "error" });
        //    }
        //    int myId = Int32.Parse(cid);
        //    try
        //    {
        //        var applications = _applicantService.GetApplication(id);
        //        if (applications == null)
        //        {
        //            return Ok(new { message = "not_found" });
        //        }
        //        if (applications.Status != 0)
        //        {
        //            return Ok(new { message = "delete_fail" });
        //        }
        //        else {
        //            _applicantService.DeleteApplication(applications);
        //            return Ok(new { message = "delete_successfull" });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest(new { error = "error" });
        //    }
        //}

        [Authorize]
        [HttpDelete("DeleteJobApply/{id}")]
        public IActionResult DeleteMyJobApplyPending(int id)
        {
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            if (cid == null)
            {
                return BadRequest(new { error = "error" });
            }
            int myId = Int32.Parse(cid);
            try
            {
                var applications = _applicantService.GetApplication(id);
                if (applications == null)
                {
                    return Ok(new { message = "not_found" });
                }
                if (applications.Status != 0)
                {
                    return Ok(new { message = "delete_fail" });
                }
                else
                {
                    _applicantService.DeleteApplication(applications);
                    return Ok(new { message = "delete_successfull" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("GetJobDetail/{id}")]
        public IActionResult GetMyJobDetail(int id)
        {
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            if (cid == null)
            {
                return BadRequest(new { error = "error" });
            }
            int myId = Int32.Parse(cid);
            try
            {
                var applications = _applicantService.GetApplication(id);
                if (applications == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    var app = _mapper.Map<ApplicationJobDTO>(applications);
                    return Ok(app);
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("SetDone/{id}")]
        public async Task< IActionResult> SetDoneJob(int id)
        {
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            if (cid == null)
            {
                return BadRequest(new { error = "error" });
            }
            int myId = Int32.Parse(cid);
            try
            {
                var applications = _applicantService.GetApplication(id);
                if (applications == null)
                {
                    return Ok(new { message = "not_found" });
                }
                if (applications.Status == 0)
                {
                    return Ok(new { message = "job_not_been_accepted" });
                }
                else if (applications.Status == 2)
                {
                    return Ok(new { message = "job_has_done" });
                }
                else if (applications.Status == 1)
                {
                    var customer = _customerService.GetCustomerById(myId);
                    applications.Status = 2;
                    _applicantService.UpdateApplication(applications);
                    string content = customer.FullName + " đã hoàn thành công việc " + applications.Job.Title + " của bạn.";
                    var notification = _notificationService.AddNotification(content, applications.Job.JobAssignerId.ToString(), applications.JobId.ToString(), id.ToString());
                    await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                    return Ok(new { message = "set_done_successful" });
                }
                else
                {
                    return BadRequest(new { error = "error" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("CheckApplyJob/{jobid}")]
        public IActionResult CheckApply(int jobid)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (cid == null)
                {
                    return BadRequest(new { error = "error" });
                }
                var job = _jobService.getJobById(jobid);
                if (job == null)
                {
                    return BadRequest(new { error = "error" });
                }
                else
                {
                    var check = _applicantService.CheckApplication(jobid, Int32.Parse(cid));
                    if (check != null)
                    {
                        return Ok(new { Message = "job_has_apply" });
                    }
                    else
                    {
                        return Ok(new { Message = "job_not_apply" });
                    }
                }
               
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [Authorize]
        [HttpPost("CancelJob/{id}")]

        public IActionResult CancelJob(int id)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var app = _applicantService.GetApplication(id);
                if (app == null)
                {
                    return BadRequest(new { error = "error" });
                }
                if(app.Status != 0)
                {
                    return Ok(new {Message = "can_not_delete"});
                }
                else
                {
                    _applicantService.DeleteAppication(app);
                    return Ok(new { Message = "delete_successfull" });
                }

            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [Authorize]
        [HttpPost("ApplyJob")]
        public async Task<IActionResult> ApplyJob(ApplyDTO ApplyDTO)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var customer = _customerService.GetCustomerById(Int32.Parse(cid));
                var check = _applicantService.CheckApplication(ApplyDTO.JobId, Int32.Parse(cid));
                if (check != null)
                {
                    return Ok(new { Message = "job_has_apply" });
                }
                if (cid == null)
                {
                    return BadRequest(new { error = "error" });
                }
                if (customer.FrofileStatus == 0)
                {
                    return Ok(new { message = "please_update_profile" });
                }
                else
                {
                    var job = _jobService.getJobById(ApplyDTO.JobId);
                    if (job == null)
                    {
                        return Ok(new { Message = "not_found" });
                    }
                    if (job.Status == 1 && job.JobAssignerId != Int32.Parse(cid))
                    {
                        //NewApplyRequestDTO app = new NewApplyRequestDTO();
                        Application app = new Application();
                        app.JobId = ApplyDTO.JobId;
                        app.ApplicantId = Int32.Parse(cid);
                        app.ApplicationDate = DateTime.Now;
                        app.StartDate = job.StartDate;
                        app.EndDate = job.EndDate;
                        app.Money = job.Money;
                        app.Status = 0;
                        app = _applicantService.CreateApplication(app);
                        if(job.IsSendMail == true) {
                            _applicantService.SendMailApply(job);
                        }
                        string content = customer.FullName + " đã apply vào công việc " + job.Title + " của bạn.";
                        var notification = _notificationService.AddNotification(content, job?.JobAssignerId.ToString(), ApplyDTO.JobId.ToString(), app.Aid.ToString());
                        await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                        //_applicantService.CreateApplication(_mapper.Map<Application>(app));
                        return Ok(new { message = "apply_successful" });
                    }
                    else if (job.Status != 1)
                    {
                        // Job is not approve
                        return Ok(new { message = "job_not_approve" });
                    }
                    else
                    {
                        //The person posting the job cannot be the recipient of the job
                        return Ok(new { message = "apply_fail" });
                    }

                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }
    }
}
