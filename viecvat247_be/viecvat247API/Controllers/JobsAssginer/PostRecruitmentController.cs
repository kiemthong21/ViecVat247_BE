using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PostRecruitmentController : ControllerBase
    {
        private ICustomerService _customerService = new CustomerService();
        private ITransactionService _transactionService = new TransactionService();

        private IJobService _jobService = new JobService();
        private ISkillService _skillService = new SkillService();
        private readonly IMapper _mapper;
        public PostRecruitmentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("ListAllJobs")]
        public IActionResult getAllJob([FromQuery] string? cid, string? searchValue, string? orderBy,
            int pageIndex, int pageSize, string? typesJobs)
        {
            try
            {
                PaginatedList<Job> jobs = _jobService.GetAllJob(cid, searchValue, pageIndex, pageSize, orderBy, typesJobs);
                return Ok(new { totalItems = jobs.Totalsize, totalPage = jobs.TotalPages, Jobs = _mapper.Map<List<JobDetailDTO>>(jobs) });

            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
            }

        [Authorize]
        [HttpGet("ListAllJobsByCustomer")]
        public IActionResult getAllJobsByCustomerId([FromQuery] string? cid, string? searchValue, string? orderBy,
            int pageIndex, int pageSize, string? typesJobs, string? status)
        {
            try
            {
                var customerId = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                //var customerId = 2;
                PaginatedList<Job> jobs = _jobService.GetAllJob(cid, searchValue, pageIndex, pageSize, orderBy, int.Parse(customerId.ToString()), typesJobs, status);
                return Ok(new { totalItems = jobs.Totalsize, totalPage = jobs.TotalPages, Jobs = _mapper.Map<List<JobDetailDTO>>(jobs) });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [AllowAnonymous]
        [HttpGet("ListAllSkillByJob")]
        public IActionResult getAllSkillByJob(int jid)
        {

            List<Skill> skills = _skillService.GetAllSkillByJobId(jid);
            return Ok(_mapper.Map<List<SkillDTO>>(skills));
        }

        [AllowAnonymous]
        [HttpGet("getJobsById")]
        public IActionResult GetJobsById([Required] int jid)
        {
            try
            {
                Job job = _jobService.getJobById(jid);
                List<Skill> skills = _skillService.GetAllSkillByJobId(jid);
                List<SkillDTO> skillDTOs = _mapper.Map<List<SkillDTO>>(skills);
                JobDetailDTO JobDetailDTO = _mapper.Map<JobDetailDTO>(job);
                JobDetailDTO.Skills = new List<int>();
                foreach (SkillDTO item in skillDTOs)
                {
                    JobDetailDTO.Skills.Add(item.SkillId);
                    JobDetailDTO.SkillCategoryId = item.SkillCategoryId;
                }
                 
                return Ok(JobDetailDTO);
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }

        }

        [Authorize]
        [HttpPost("CreateJobs")]
        public IActionResult createJob(JobCreateDTO job)
        {
            try
            {
                string[] listSkill = job.ListSkill;
                foreach (string skill in listSkill)
                {
                    try
                    {
                        if (_skillService.GetSkill(int.Parse(skill)) == null)
                        {
                            return Ok(new { message = "skill_not_found" });
                        }
                    }
                    catch (Exception)
                    {
                        return Ok(new { message = "skill_not_found" });
                    }
                    
                }
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
                Job jobCreate = _mapper.Map<Job>(job);
                jobCreate.StartDate = DateTime.Now;
                jobCreate.JobAssignerId = int.Parse(cid);
                jobCreate.Image = cus.Avatar;
                if (cus.Epoint >= 3000 && jobCreate.Status != 5)
                {
                    cus.Epoint = cus.Epoint - 3000;
                    Job jobnew = _jobService.CreateJob(jobCreate);
                    _jobService.CreateJobSkill(listSkill, jobnew.JobsId);
                    _customerService.UpdateCustomer(cus);
                    //them hàm update transaction
                    Transaction transactionCreate = new Transaction();
                    transactionCreate.Paymentdate = DateTime.Now;
                    transactionCreate.CustomerId = int.Parse(cid);
                    transactionCreate.JobId = jobnew.JobsId;
                    transactionCreate.Epoint = 3000;
                    transactionCreate.Status = 1;
                    transactionCreate.Note = "Tao_Cong_Viec_3000";
                    transactionCreate.Detail = "Tao_Cong_Viec"+ DateTime.Now;
                    transactionCreate.BankCode = "He_Thong_Tu_Dong";
                    transactionCreate.TransactionType = 3;
                    transactionCreate.OldBalance = cus.Epoint + 3000;
                    transactionCreate.NewBalance = cus.Epoint ;
                    _transactionService.CreateTransaction(transactionCreate);
                    return Ok(new { message = "create_successful" });
                }else if (jobCreate.Status == 5)
                {
                    Job jobnew = _jobService.CreateJob(jobCreate);
                    _jobService.CreateJobSkill(listSkill, jobnew.JobsId);
                    return Ok(new { message = "create_draf_job_successful" });
                }
                else
                {
                    return Ok(new { message = "insufficient_balance" });
                }

            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }

        }

        [Authorize]
        [HttpPut("UpdateJobs")]
        public IActionResult UpdateJob(JobUpdateDTO jobUpdateDTO)
        {
            try
            {
                string[] listSkill = jobUpdateDTO.ListSkill;
                foreach (string skill in listSkill)
                {
                    try
                    {
                        if (_skillService.GetSkill(int.Parse(skill)) == null)
                        {
                            return Ok(new { message = "skill_not_found" });
                        }
                    }
                    catch (Exception)
                    {
                        return Ok(new { message = "skill_not_found" });
                    }

                }
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
                Job job = _jobService.getJobById(jobUpdateDTO.JobsId);
                Job jobUpdate = _mapper.Map<Job>(jobUpdateDTO);
                jobUpdate.JobAssignerId = int.Parse(cid);
                jobUpdate.StartDate = job.StartDate;
                jobUpdate.Image = cus?.Avatar;
                _jobService.UpdateJob(jobUpdate);
                
                _jobService.DeleteJobSkillbyJobId(jobUpdateDTO.JobsId);
                _jobService.CreateJobSkill(listSkill, jobUpdateDTO.JobsId);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }

        }

        [Authorize]
        [HttpPut("PostJobsDraff")]
        public IActionResult PostJobsDraff(int id)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
                Job job = _jobService.getJobById(id);
                if (cus.Epoint >= 3000)
                {
                    job.Status = 0;
                    cus.Epoint = cus.Epoint - 3000;
                    _jobService.UpdateJob(job);
                    _customerService.UpdateCustomer(cus);
                    //them hàm update transaction
                    Transaction transactionCreate = new Transaction();
                    transactionCreate.Paymentdate = DateTime.Now;
                    transactionCreate.CustomerId = int.Parse(cid);
                    transactionCreate.JobId = job.JobsId;
                    transactionCreate.Epoint = 3000;
                    transactionCreate.Status = 1;
                    transactionCreate.Note = "Tao_Cong_Viec_3000";
                    transactionCreate.Detail = "Tao_Cong_Viec" + DateTime.Now;
                    transactionCreate.BankCode = "He_Thong_Tu_Dong";
                    transactionCreate.TransactionType = 3;
                    transactionCreate.OldBalance = cus.Epoint + 3000;
                    transactionCreate.NewBalance = cus.Epoint;
                    _transactionService.CreateTransaction(transactionCreate);
                    return Ok(new { message = "create_successful" });
                }
                else
                {
                    return Ok(new { message = "insufficient_balance" });
                }
                
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }

        }

    }
}
