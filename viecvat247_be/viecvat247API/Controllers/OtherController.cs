using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers
{
    [Route("api")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly IConfiguration _config;
        private ISkillService _skillService = new SkillService();
        private IOtherService _otherService = new OtherService();
        private IApplicantService _applicantService = new ApplicantService();
        private readonly IMapper _mapper;
        public OtherController(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        [DisableRequestSizeLimit]
        [HttpPost("Other/upload-image")]
        // up image on cloundinary
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            //Get key account cloudinary in file appsetting
            string CLOUND_NAME = _config.GetSection("CLOUND_NAME").Value;
            string CLOUND_KEY = _config.GetSection("CLOUND_KEY").Value;
            string API_SECRET = _config.GetSection("API_SECRET").Value;


            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(CLOUND_NAME, CLOUND_KEY, API_SECRET);
            try
            {
                //The uploaded file format is incorrect. Please choose images in .jpg, .jpeg, .png, .gif, .webp formats
                if (ValidateImage(file) == false)
                {
                    return BadRequest(new { error = "wrong_format" });
                }
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {

                        var uploadParams = new ImageUploadParams
                        {
                            File = new CloudinaryDotNet.FileDescription(file.FileName, stream),
                        };
                        Cloudinary cloudinary = new Cloudinary(account);

                        //upload file
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        return Ok(new { imageUrl = uploadResult?.SecureUri?.AbsoluteUri });
                    }
                }
                else
                {
                    return Ok(new { message = "file_null" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost("Other/upload-multiple")]
        public async Task<IActionResult> UploadMultipleImages([FromForm] List<IFormFile> files)
        {
            // Thực hiện xử lý cho mỗi ảnh trong danh sách
            List<string> imageUrls = new List<string>();
            string CLOUND_NAME = _config.GetSection("CLOUND_NAME").Value;
            string CLOUND_KEY = _config.GetSection("CLOUND_KEY").Value;
            string API_SECRET = _config.GetSection("API_SECRET").Value;
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(CLOUND_NAME, CLOUND_KEY, API_SECRET);

            foreach (var file in files)
            {
                if (ValidateImage(file) == false)
                {
                    return BadRequest(new { error = "wrong_format" });
                }
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new CloudinaryDotNet.FileDescription(file.FileName, stream),
                        };

                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        imageUrls.Add(uploadResult?.SecureUri?.AbsoluteUri);
                    }
                }
            }

            return Ok(new { imageUrls });
        }

        [HttpGet("Other/validate-fileimage")]
        public bool ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                //File is null
                return false;
            }

            // Kiểm tra định dạng tệp
            string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!Array.Exists(validImageExtensions, ext => ext == fileExtension))
            {
                //File format not supported
                return false;
            }

            try
            {
                using (var imageStream = file.OpenReadStream())
                {
                    // Kiểm tra xem tệp có thuộc tính hình ảnh không
                    using (var image = SixLabors.ImageSharp.Image.Load(imageStream))
                    {
                        if (image.Width > 0 && image.Height > 0)
                        {
                            //The uploaded file is a valid image file
                            return true;
                        }
                        else
                        {
                            //The file is not a valid image.
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //An error occurred when checking the image file
                return false;
            }
        }

        [AllowAnonymous]
        [HttpGet("Skill/GetAll")]
        public IActionResult GetSkills([FromQuery] string? searchValue, string? cate, string? orderBy)
        {
            try
            {

                PaginatedList<Skill> skills = _otherService.GetAllSkills(searchValue, cate, orderBy);
                var skillsDTO = skills.Select(e => _mapper.Map<SkillDTO>(e));
                return Ok(new { totalItems = skills.Totalsize, totalPages = skills.TotalPages, Skills = skillsDTO });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        

        //Get All skill category
        [AllowAnonymous]
        [HttpGet("SkillCategory/GetAll")]
        public IActionResult GetSkillCategories([FromQuery] string? searchValue, string? orderBy)
        {
            try
            {

                PaginatedList<SkillCategory> skills = _otherService.GetSkillCategories(searchValue, orderBy);
                var skillsCateDTO = skills.Select(e => _mapper.Map<SkillCategoryDTO>(e));
                return Ok(new { totalItems = skills.Totalsize, totalPages = skills.TotalPages, SkillsCategory = skillsCateDTO });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [AllowAnonymous]
        [HttpGet("JobsCategory/GetAll")]
        public IActionResult GetJobsCategories([FromQuery] string? searchValue, string? orderBy)
        {
            try
            {

                PaginatedList<JobsCategory> cate = _otherService.GetJobCategory(searchValue, orderBy);
                var cateDTO = cate.Select(e => _mapper.Map<JobsCategoryDTO>(e));
                return Ok(new { totalItems = cate.Totalsize, totalPages = cate.TotalPages, JobsCategory = cate });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [AllowAnonymous]
        [HttpGet("Report/GetFeedbacksByCid")]
        public IActionResult GetFeedbacksByCid([FromQuery] int cid, int pageIndex, int pageSize)
        {
            try
            {
                PaginatedList<Application> apps = _applicantService.GetReportsByCid(cid, pageIndex, pageSize);
                return Ok(new { totalItems = apps.Totalsize, totalPage = apps.TotalPages, Jobs = _mapper.Map<List<FeedbackDTO>>(apps) });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [AllowAnonymous]
        [HttpGet("Report/GetNumberFeedbackByCid")]
        public IActionResult GetNumberFeedbackByCid([FromQuery] int cid)
        {
            try
            {
                int num = _applicantService.GetNumberFeedbackByCid(cid);
                return Ok(new { NumberFeedback = num });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }
    }
}
