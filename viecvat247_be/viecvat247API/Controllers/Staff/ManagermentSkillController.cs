using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Staff
{
    [Route("api/Staff")]
    [ApiController]
    public class ManagermentSkillController : ControllerBase
    {

        private ISkillService _skillService = new SkillService();

        private readonly IMapper _mapper;
        public ManagermentSkillController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("Skill/Add")]
        public IActionResult AddNewSkill(NewSkillDTO skillDTO)
        {
            try
            {
                Skill skill = _mapper.Map<Skill>(skillDTO);
                skill.Status = 1;
                if (_skillService.GetSkill(skillDTO!.SkillName) != null)
                {
                    return Ok(new { message = "already_have_skills" });
                }
                else
                {
                    _skillService.AddNewSkill(skill);
                    return Ok(new { message = "add_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "add_fail" });
            }
        }

        [Authorize]
        [HttpPost("SkillCategory/Add")]
        public IActionResult AddNewSkillCategory(NewSkillCategoryDTO cateDTO)
        {
            try
            {
                if (_skillService.GetSkillCategory(cateDTO.SkillCategoryName) != null)
                {
                    return Ok(new { message = "already_have_skillCategory" });
                }
                else
                {
                    SkillCategory cate = _mapper.Map<SkillCategory>(cateDTO);
                    _skillService.AddNewSkillCategory(cate);
                    return Ok(new { message = "add_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "add_fail" });
            }

        }

        [Authorize]
        [HttpGet("Skill/{id}")]
        public IActionResult GetSkill(int id)
        {
            try
            {
                Skill skill = _skillService.GetSkill(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                SkillDTO skillDTO = _mapper.Map<SkillDTO>(skill);
                return Ok(skillDTO);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        //Get skill category detail
        [Authorize]
        [HttpGet("SkillCategory/{id}")]
        public IActionResult GetSkillCategory(int id)
        {
            try
            {
                SkillCategory skill = _skillService.GetSkillCategory(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                SkillCategoryDTO skillDTO = _mapper.Map<SkillCategoryDTO>(skill);
                return Ok(skillDTO);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        //Get All skill
        [AllowAnonymous]
        [HttpGet("Skill/GetAll")]
        public IActionResult GetSkills([FromQuery] string? searchValue, string? cate, string? orderBy,
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
                    PaginatedList<Skill> skills = _skillService.GetAllSkills(searchValue, cate, pageIndex, pageSize, orderBy);
                    var skillsDTO = skills.Select(e => _mapper.Map<SkillDTO>(e));
                    return Ok(new { totalItems = skills.Totalsize, totalPages = skills.TotalPages, Skills = skillsDTO });
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

        //Get All skill category
        [AllowAnonymous]
        [HttpGet("SkillCategory/GetAll")]
        public IActionResult GetSkillCategories([FromQuery] string? searchValue, string? orderBy,
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
                    PaginatedList<SkillCategory> skills = _skillService.GetSkillCategories(searchValue, pageIndex, pageSize, orderBy);
                    var skillsCateDTO = skills.Select(e => _mapper.Map<SkillCategoryDTO>(e));
                    return Ok(new { totalItems = skills.Totalsize, totalPages = skills.TotalPages, SkillsCategory = skillsCateDTO });
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
        [HttpPut("Skill/Update/{id}")]
        public IActionResult UpdateSkill(int id, NewSkillDTO newSkill)
        {
            try
            {
                var skill = _skillService.GetSkill(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                if (!newSkill.SkillName.Equals(skill.SkillName)){
                    if (_skillService.GetSkill(newSkill!.SkillName) != null)
                    {
                        return Ok(new { message = "already_have_skills" });
                    }
                }
                skill.SkillName = newSkill.SkillName;
                skill.SkillCategoryId = newSkill.SkillCategoryId;
                skill.Description = newSkill.Description;
                _skillService.UpdateSkill(skill);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "update_fail" });
            }
        }

        [Authorize]
        [HttpPut("SkillCategory/Update/{id}")]
        public IActionResult UpdateSkillCategory(int id, NewSkillCategoryDTO newCate)
        {
            try
            {
                var skill = _skillService.GetSkillCategory(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                if (!newCate.SkillCategoryName.Equals(skill.SkillCategoryName))
                {
                    if (_skillService.GetSkillCategory(newCate.SkillCategoryName) != null)
                    {
                        return Ok(new { message = "already_have_skillcatgory" });
                    }
                }
                skill.SkillCategoryName = newCate.SkillCategoryName;
                skill.Description = newCate.Description;
                _skillService.UpdateSkillCatgory(skill);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "update_fail" });
            }
        }

        [Authorize]
        [HttpDelete("Skill/Delete/{id}")]
        public IActionResult DeleteSkill(int id)
        {
            try
            {
                var skill = _skillService.GetSkill(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    _skillService.DeleteSkill(skill);
                    return Ok(new { message = "delete_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "delete_fail" });
            }
        }

        [Authorize]
        [HttpDelete("SkillCategory/Delete/{id}")]
        public IActionResult DeleteSkillCategory(int id)
        {
            try
            {
                var skill = _skillService.GetSkillCategory(id);
                if (skill == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    _skillService.DeleteSkillCategory(skill);
                    return Ok(new { message = "delete_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "delete_fail" });
            }
        }


    }
}
