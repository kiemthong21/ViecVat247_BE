using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Staff
{
    [Route("api/Staff")]
    [ApiController]
    public class ManagermentJobsCategoryController : ControllerBase
    {
        private IJobCategoryService _jobCateService = new JobCategoryService();

        private readonly IMapper _mapper;
        public ManagermentJobsCategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [Authorize]
        [HttpPost("JobsCategory/Add")]
        public IActionResult AddNewJobsCategory(NewCategoryJobDTO cate)
        {
            try
            {
                if (_jobCateService.GetJobsCategory(cate.JobCategoryName) != null)
                {
                    return Ok(new { message = "already_have_jobcategory" });
                }
                var jobcates = _mapper.Map<JobsCategory>(cate);
                jobcates.Status = 1;
                _jobCateService.AddNewJobsCategory(jobcates);
                return Ok(new { message = "add_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "add_fail" });
            }
        }

        [Authorize]
        [HttpGet("JobsCategory/{id}")]
        public IActionResult GetJobCategory(int id)
        {
            try
            {
                JobsCategory cate = _jobCateService.GetJobsCategory(id);
                if (cate == null)
                {
                    return Ok(new { message = "not_found" });
                }
                return Ok(_mapper.Map<JobsCategoryDTO>(cate));
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        //Get All skill
        [Authorize]
        [HttpGet("JobsCategory/GetAll")]
        public IActionResult GetJobsCategories([FromQuery] string? searchValue, string? orderBy,
            int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                try
                {
                    if (pageIndex < 1 || pageSize < 1)
                    {
                        BadRequest(new { error = "error" });
                    }
                    PaginatedList<JobsCategory> cate = _jobCateService.GetJobsCategories(searchValue, pageIndex, pageSize, orderBy);
                    var cateDTO = cate.Select(e => _mapper.Map<JobsCategoryDTO>(e));
                    return Ok(new { totalItems = cate.Totalsize, totalPages = cate.TotalPages, JobsCategory = cateDTO });
                }
                catch (Exception)
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
        [HttpPut("JobsCategory/Update/{id}")]
        public IActionResult UpdateJobCategory(int id, NewCategoryJobDTO newCate)
        {
            try
            {
                var cate = _jobCateService.GetJobsCategory(id);
                if (cate == null)
                {
                    return Ok(new { message = "not_found" });
                }
                if(!cate.JobCategoryName.Equals(newCate.JobCategoryName))
                {
                    if (_jobCateService.GetJobsCategory(newCate.JobCategoryName) != null)
                    {
                        return Ok(new { message = "already_have_jobcategory" });
                    }
                }
                cate.JobCategoryName = newCate.JobCategoryName;
                cate.Description = newCate.Description;
                _jobCateService.UpdateJobCategory(cate);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "update_fail" });
            }
        }

        [Authorize]
        [HttpDelete("JobsCategory/Inactive/{id}")]
        public IActionResult InActiveJobsCategory(int id)
        {
            try
            {
                var cate = _jobCateService.GetJobsCategory(id);
                if (cate == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    cate.Status = 0;
                    _jobCateService.UpdateJobCategory(cate);
                    return Ok(new { message = "Inactive_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "delete_fail" });
            }
        }

        [Authorize]
        [HttpDelete("JobsCategory/Active/{id}")]
        public IActionResult ActiveJobsCategory(int id)
        {
            try
            {
                var cate = _jobCateService.GetJobsCategory(id);
                if (cate == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    cate.Status = 1;
                    _jobCateService.UpdateJobCategory(cate);
                    return Ok(new { message = "active_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "delete_fail" });
            }
        }
    }
}
