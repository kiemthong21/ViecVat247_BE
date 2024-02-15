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

namespace viecvat247API.Controllers.JobsAssginer
{
    [Route("api/JobAssigner")]
    [ApiController]
    public class ManageJobApplyController : ControllerBase
    {
        private IJobService _jobService = new JobService();
        private ICustomerService _customerService = new CustomerService();
        private IApplicantService _applicantService = new ApplicantService();
        private INotificationService _notificationService = new NotificationService();
        private readonly IMapper _mapper;
        private IHubContext<NotificationHub> _hubContext;

        public ManageJobApplyController(IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet("MyJob")]
        public IActionResult GetMyJob([FromQuery] string? cid, string? searchValue, string? orderBy,
            int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (uid == null)
                {
                    return BadRequest(new { error = "error" });
                }
                int myId = int.Parse(uid);
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest(new { error = "error" });
                }
                PaginatedList<Job> job = _jobService.GetAllJob(uid, cid, searchValue, pageIndex, pageSize, orderBy, "1");
                var jobDTO = job.Select(e => _mapper.Map<JobDetailDTO>(e));
                return Ok(new { totalItems = job.Totalsize, totalPage = job.TotalPages, Jobs = jobDTO });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("ListCandidate/{jobId}")]
        public IActionResult GetListAppy(int jobId, [FromQuery] string? searchValue, string? orderBy, string? status,
            int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                Job job = _jobService.getJobById(jobId);
                if (job.JobAssignerId != Int32.Parse(uid))
                {
                    return Ok(new { message = "job_is_not_yours" });
                }
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest(new { error = "error" });
                }
                PaginatedList<Application> customer = _applicantService.GetCustomersApplyByJob(jobId, searchValue, pageIndex, pageSize, orderBy, status);
                var cus = customer.Select(e => _mapper.Map<CustomerApplyDTO>(e));
                return Ok(new { totalItems = customer.Totalsize, totalPage = customer.TotalPages, Customers = cus });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [Authorize]
        [HttpPut("ReceiveCandidates/{id}")]
        public async Task<IActionResult> GetReceiveCandidates(int id)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var app = _applicantService.GetApplication(id);
                if (app == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    if (app.Job.JobAssignerId != Int32.Parse(uid.ToString()))
                    {
                        return Ok(new { message = "job_is_not_yours" });
                    }
                    else if (app.Status == 1 || app.Status == 2)
                    {
                        return Ok(new { message = "user_has_apply_successful" });
                    }
                    else if (app.Status == 3)
                    {
                        return Ok(new { message = "user_has_reject" });
                    }
                    else if (app.Status == 4)
                    {
                        return Ok(new { message = "user_has_declined" });
                    }
                    else if (app.Status == 0)
                    {
                        var count = _applicantService.CountApplySuccessByJob(app.JobId);
                        if (count < 0)
                        {
                            return BadRequest(new { error = "error" });
                        }
                        else if (count >= app.Job.NumberPerson)
                        {
                            return Ok(new { message = "job_has_full_slot_apply" });
                        }
                        else if (count == app.Job.NumberPerson - 1)
                        {
                            Job job = app.Job;
                            job.Status = 6;
                            _jobService.UpdateJob(job);
                            app.Status = 1;
                            _applicantService.UpdateApplication(app);
                            _applicantService.RejectAllApplicationPendingByJob(app.JobId);
                            _applicantService.SendMailAppySuccessfull(app.Applicant.Cemail, app.JobId.ToString(), app.Job.Title);
                            string content = "Bạn đã apply vào công việc " + app.Job.Title + " thành công";
                            var notification = _notificationService.AddNotification(content, app.ApplicantId.ToString(), app.JobId.ToString(), id.ToString());
                            await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                            return Ok(new { message = "apply_successfull" });

                        }
                        else
                        {
                            app.Status = 1;
                            _applicantService.UpdateApplication(app);
                            _applicantService.SendMailAppySuccessfull(app.Applicant.Cemail, app.JobId.ToString(), app.Job.Title);
                            string content = "Bạn đã apply vào công việc " + app.Job.Title + " thành công";
                            var notification = _notificationService.AddNotification(content, app.ApplicantId.ToString(), app.JobId.ToString(), id.ToString());
                            await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                            return Ok(new { message = "apply_successfull" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { error = "error" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("ReApply/{id}")]
        public async Task<IActionResult> ReApply(int id)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var app = _applicantService.GetApplication(id);
                if (app == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    if (app.Job.JobAssignerId != Int32.Parse(uid.ToString()))
                    {
                        return Ok(new { message = "job_is_not_yours" });
                    }
                    else if (app.Status == 1 || app.Status == 2)
                    {
                        return Ok(new { message = "user_has_apply_successful" });
                    }
                    else if (app.Status == 4)
                    {
                        return Ok(new { message = "user_has_declined" });
                    }
                    else if(app.Status == 0 || app.Status == 3)
                    {
                        var count = _applicantService.CountApplySuccessByJob(app.JobId);
                        if (count < 0)
                        {
                            return BadRequest(new { error = "error" });
                        }
                        else if (count >= app.Job.NumberPerson)
                        {
                            return Ok(new { message = "job_has_full_slot_apply" });
                        }
                        else if (count == app.Job.NumberPerson - 1)
                        {
                            Job job = app.Job;
                            job.Status = 6;
                            _jobService.UpdateJob(job);
                            app.Status = 1;
                            _applicantService.UpdateApplication(app);
                            _applicantService.RejectAllApplicationPendingByJob(app.JobId);
                            _applicantService.SendMailAppySuccessfull(app.Applicant.Cemail, app.JobId.ToString(), app.Job.Title);
                            string content = "Bạn đã apply vào công việc " + app.Job.Title + " thành công";
                            var notification = _notificationService.AddNotification(content, app.ApplicantId.ToString(), app.JobId.ToString(), id.ToString());
                            await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                            return Ok(new { message = "apply_successfull" });

                        }
                        else
                        {
                            app.Status = 1;
                            _applicantService.UpdateApplication(app);
                            _applicantService.SendMailAppySuccessfull(app.Applicant.Cemail, app.JobId.ToString(), app.Job.Title);
                            string content = "Bạn đã apply vào công việc " + app.Job.Title + " thành công";
                            var notification = _notificationService.AddNotification(content, app.ApplicantId.ToString(), app.JobId.ToString(), id.ToString());
                            await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                            return Ok(new { message = "apply_successfull" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { error = "error" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("RejectCandidates/{id}")]
        public IActionResult RejectCandidates(int id)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var app = _applicantService.GetApplication(id);
                if (app == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    if (app.Job.JobAssignerId != Int32.Parse(uid))
                    {
                        return Ok(new { message = "job_is_not_yours" });
                    }
                    else if (app.Status != 0)
                    {
                        return Ok(new { message = "user_has_process" });
                    }
                    else
                    {
                        app.Status = 3;
                        _applicantService.UpdateApplication(app);
                        //string contenf = "Bạn đã apply vào công việc " + app.Job.Title + " thành công";
                        //_notificationService.AddNotification("Bạn đã app", app.ApplicantId.ToString(), app.JobId.ToString());
                        //await NotificationHub.UpdateNotification(_hubContext, uid);
                        return Ok(new { message = "reject_successfull" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("SendFeedback/{id}")]
        public IActionResult SendFeedBack(int id, EvaluateDTO evaluate)
        {
            try {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var application = _applicantService.GetApplication(id);
                if (application == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    if (application.Job.JobAssignerId != Int32.Parse(uid))
                    {
                        return Ok(new { message = "job_is_not_yours" });
                    }
                    if(application.Status != 2)
                    {
                        return Ok(new { message = "worked_is_not_done" });
                    }
                    else
                    {
                        application.Status = 2;
                        application.Feedback = evaluate.comment;
                        application.Voting = evaluate.rate;
                        application.EndDate = DateTime.Now;
                        application.RateDate = DateTime.Now;
                        _applicantService.UpdateApplication(application);
                        return Ok(new { message = "user_has_process" });
                    }
                }
               
            } catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("SetJobDone/{jobId}")]
        public IActionResult EditStatusJob(int jobId)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                Job job = _jobService.getJobById(jobId);
                if (job.JobAssignerId != Int32.Parse(uid))
                {
                    return Ok(new { message = "job_is_not_yours" });
                }
                if (job.Status == 0)
                {
                    return Ok(new { message = "job_wait_approve" });
                }else if(job.Status == 3)
                {
                    return Ok(new { message = "job_has_reject" });
                }else if(job.Status == 4)
                {
                    return Ok(new { message = "job_has_done" });
                }else if(job.Status == 5)
                {
                    return Ok(new { message = "job_being_draff" });
                }else if(job.Status == 2)
                {
                    return Ok(new { message = "job_is_pending" });
                }
                else
                {
                    job.Status = 4;
                    job.EndDate = DateTime.Now;
                    _jobService.UpdateJob(job);
                    return Ok(new { message = "set_done_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("CancelJobSeekerApprove/{id}")]
        public async Task<IActionResult> CancelJob(int id)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var app = _applicantService.GetApplication(id);
                if (app == null)
                {
                    return BadRequest(new { error = "error" });
                }
                if (app.Status != 1)
                {
                    return Ok(new { message = "not_approve_yet" });
                }
                else
                {
                    app.Status = 3;
                    _applicantService.UpdateApplication(app);
                    _applicantService.SendMailCancelJobAssigner(app.Applicant.Cemail, app.Job.Title);
                    string content = "Công việc " + app.Job.Title + " của bạn đã bị hủy. ";
                    var notification = _notificationService.AddNotification(content, app.ApplicantId.ToString(), app.JobId.ToString(), id.ToString());
                    await NotificationHub.UpdateNotification(_hubContext, notification.CustomerId.ToString());
                    return Ok(new { message = "cancel_successfull" });
                }

            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("NumberSeekerDone/{jobId}")]
        public IActionResult NumberSeekerDone(int jobId)
        {
            try
            {
                var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                Job job = _jobService.getJobById(jobId);
                if (job.JobAssignerId != Int32.Parse(uid))
                {
                    return Ok(new { message = "job_is_not_yours" });
                }
                int NumberSeekerApply = _applicantService.getNumberSeekerApplyByJobId(jobId);
                int NumberSeekerDone = _applicantService.getNumberSeekerApplyByStatus(jobId,2);
                return Ok(new { NumberSeekerApply = NumberSeekerApply, NumberSeekerDone = NumberSeekerDone });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }
    }
}

