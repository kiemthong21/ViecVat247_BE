using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.MigrationData
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDataController : ControllerBase
    {
        private ISkillService _skillService = new SkillService();
        private IOtherService _otherService = new OtherService();
        private ICustomerService _customerService = new CustomerService();
        private readonly IMapper _mapper;
        public ExportDataController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Skill")]
        public async Task<IActionResult> ExportDataSkill()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Skill");
                    worksheet.Cells["A1"].Value = "Skill id";
                    worksheet.Cells["B1"].Value = "Skill Name";
                    worksheet.Cells["C1"].Value = "SkillCategoryName";
                    worksheet.Cells["D1"].Value = "Descrition";
                    worksheet.Cells["E1"].Value = "Status";

                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 100;
                    worksheet.Column(5).Width = 20;
                    List<Skill> Skills = new List<Skill>();
                    List<SkillDTO> SkillDTO = new List<SkillDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            Skills = context.Skills
                                .Include(c => c.SkillCategory)
                                .ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (Skills != null)
                    {
                        SkillDTO = Skills.Select(e => _mapper.Map<SkillDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int rowActive = 0;
                    int rowInActive = 0;

                    foreach (SkillDTO c in SkillDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.SkillId;
                        worksheet.Cells["B" + row].Value = c.SkillName;
                        worksheet.Cells["C" + row].Value = c.SkillCategoryName;
                        worksheet.Cells["D" + row].Value = c.Description;
                        if (c.Status == 1)
                        {
                            worksheet.Cells["E" + row].Value = "Active";
                            rowActive++;
                        }
                        else
                        {
                            worksheet.Cells["E" + row].Value = "InActive";
                            rowInActive++;
                        }
                        row++;
                    }
                    if (SkillDTO != null )
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + SkillDTO.Count + " Skill.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Skill.";
                    }
                    
                    worksheet.Cells["A" + (row + 2)].Value = "Skill Active: " + rowActive+".";
                    worksheet.Cells["A" + (row + 3)].Value = "Skill InActive: " + rowInActive + ".";

                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Skill.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpGet]
        [Route("SkillCategory")]
        public async Task<IActionResult> ExportDataSkillCategory()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("SkillCategory");
                    // Đặt dữ liệu vào tệp Excel
                    worksheet.Cells["A1"].Value = "SkillCategory Id";
                    worksheet.Cells["B1"].Value = "SkillCategory Name";
                    worksheet.Cells["C1"].Value = "Description";

                    PaginatedList<SkillCategory> skills = _otherService.GetSkillCategories("", "");
                    List<SkillCategoryDTO> skillsCateDTO = skills.Select(e => _mapper.Map<SkillCategoryDTO>(e)).ToList();
                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 70;
                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    foreach (SkillCategoryDTO s in skillsCateDTO)
                    {
                        worksheet.Cells["A" + row].Value = s.SkillCategoryId;
                        worksheet.Cells["B" + row].Value = s.SkillCategoryName;
                        worksheet.Cells["C" + row].Value = s.Description;
                        row++;
                    }
                    if (skillsCateDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + skillsCateDTO.Count + " SkillCategory.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Skill.";
                    }
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SkillCategory.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpGet]
        [Route("JobsCategories")]
        public async Task<IActionResult> ExportDataJobsCategories()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("JobsCategories");
                    worksheet.Cells["A1"].Value = "JobCategory id";
                    worksheet.Cells["B1"].Value = "JobCategory Name";
                    worksheet.Cells["C1"].Value = "Descrition";
                    worksheet.Cells["D1"].Value = "Status";

                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 100;
                    worksheet.Column(4).Width = 20;
                    List<JobsCategory> JobsCategorys = new List<JobsCategory>();
                    List<JobsCategoryDTO> JobsCategoryDTO = new List<JobsCategoryDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            JobsCategorys = context.JobsCategories.ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (JobsCategorys != null)
                    {
                        JobsCategoryDTO = JobsCategorys.Select(e => _mapper.Map<JobsCategoryDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int rowActive = 0;
                    int rowInActive = 0;
                    foreach (JobsCategoryDTO c in JobsCategoryDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.JobCategoryId;
                        worksheet.Cells["B" + row].Value = c.JobCategoryName;
                        worksheet.Cells["C" + row].Value = c.Description;
                        if (c.Status == 1)
                        {
                            worksheet.Cells["D" + row].Value = "Active";
                            rowActive++;
                        }
                        else
                        {
                            worksheet.Cells["D" + row].Value = "InActive";
                            rowInActive++;
                        }
                        row++;
                    }
                    if (JobsCategoryDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + JobsCategoryDTO.Count + " JobsCategory.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 JobsCategory.";
                    }

                    worksheet.Cells["A" + (row + 2)].Value = "JobsCategory Active: " + rowActive + ".";
                    worksheet.Cells["A" + (row + 3)].Value = "JobsCategory InActive: " + rowInActive + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JobsCategories.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpGet]
        [Route("Job")]
        public async Task<IActionResult> ExportDataJob()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Job");

                    // Đặt dữ liệu vào tệp Excel
                    worksheet.Cells["A1"].Value = "Job id";
                    worksheet.Cells["B1"].Value = "JobAssigner Name";
                    worksheet.Cells["C1"].Value = "JobCategory Name";
                    worksheet.Cells["D1"].Value = "Title";
                    worksheet.Cells["E1"].Value = "Image";
                    worksheet.Cells["F1"].Value = "Job Overview";
                    worksheet.Cells["G1"].Value = "Required Skills";
                    worksheet.Cells["H1"].Value = "Preferred Skills";
                    worksheet.Cells["I1"].Value = "Notice To JobSeeker";
                    worksheet.Cells["J1"].Value = "Address";
                    worksheet.Cells["K1"].Value = "StartDate";
                    worksheet.Cells["L1"].Value = "EndDate";
                    worksheet.Cells["M1"].Value = "WorkingTime";
                    worksheet.Cells["N1"].Value = "Money";
                    worksheet.Cells["O1"].Value = "NumberPerson";
                    worksheet.Cells["P1"].Value = "TypeJobs";
                    worksheet.Cells["Q1"].Value = "Status";


                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 40;
                    worksheet.Column(5).Width = 100;
                    worksheet.Column(6).Width = 100;
                    worksheet.Column(7).Width = 100;
                    worksheet.Column(8).Width = 100;
                    worksheet.Column(9).Width = 100;
                    worksheet.Column(10).Width = 30;
                    worksheet.Column(11).Width = 25;
                    worksheet.Column(12).Width = 25;
                    worksheet.Column(13).Width = 10;
                    worksheet.Column(14).Width = 10;
                    worksheet.Column(15).Width = 10;
                    worksheet.Column(16).Width = 10;
                    worksheet.Column(17).Width = 20;
                    List<Job> Jobs = new List<Job>();
                    List<JobDetailDTO> JobDTO = new List<JobDetailDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            Jobs = context.Jobs
                                .Include(j => j.JobAssigner)
                                .Include(j => j.JobCategory).ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (Jobs != null)
                    {
                        JobDTO = Jobs.Select(e => _mapper.Map<JobDetailDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int row0 = 1;
                    int row1 = 0;
                    int row2 = 0;
                    int row3 = 0;
                    int row4 = 0;
                    int row5 = 0;
                    int row6 = 0;
                    foreach (JobDetailDTO c in JobDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.JobsId;
                        worksheet.Cells["B" + row].Value = c.JobAssignerName;
                        worksheet.Cells["C" + row].Value = c.JobCategoryName;
                        worksheet.Cells["D" + row].Value = c.Title;
                        worksheet.Cells["E" + row].Value = c.Image;
                        worksheet.Cells["F" + row].Value = c.Job_Overview;
                        worksheet.Cells["G" + row].Value = c.Required_Skills;
                        worksheet.Cells["H" + row].Value = c.Preferred_Skills;
                        worksheet.Cells["I" + row].Value = c.NoticeToJobSeeker;
                        worksheet.Cells["J" + row].Value = c.Address;

                        if (c.StartDate == null)
                        {
                            worksheet.Cells["K" + row].Value = c.StartDate;
                        }
                        else
                        {
                            DateTime StartDateTime = DateTime.SpecifyKind((DateTime)c.StartDate, DateTimeKind.Utc);
                            worksheet.Cells["K" + row].Value = StartDateTime;
                            worksheet.Cells["K" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }

                        if (c.EndDate == null)
                        {
                            worksheet.Cells["L" + row].Value = c.EndDate;
                        }
                        else
                        {
                            DateTime EndDateTime = DateTime.SpecifyKind((DateTime)c.EndDate, DateTimeKind.Utc);
                            worksheet.Cells["L" + row].Value = EndDateTime;
                            worksheet.Cells["L" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }
                        

                        worksheet.Cells["M" + row].Value = c.WorkingTime;
                        worksheet.Cells["N" + row].Value = c.Money;
                        worksheet.Cells["O" + row].Value = c.NumberPerson;
                        worksheet.Cells["P" + row].Value = c.TypeJobs;
                        if (c.Status == 0)
                        {
                            worksheet.Cells["Q" + row].Value = "Wait for approval";
                            row0++;
                        }
                        else if (c.Status == 1)
                        {
                            worksheet.Cells["Q" + row].Value = "Approve ";
                            row1++;
                        }
                        else if (c.Status == 2)
                        {
                            worksheet.Cells["Q" + row].Value = "Pending ";
                            row2++;
                        }
                        else if (c.Status == 3)
                        {
                            worksheet.Cells["Q" + row].Value = "Reject ";
                            row3++;
                        }
                        else if (c.Status == 4)
                        {
                            worksheet.Cells["Q" + row].Value = "Complete";
                            row4++;
                        }
                        else if (c.Status == 5)
                        {
                            worksheet.Cells["Q" + row].Value = "Draft";
                            row5++;
                        }
                        else if (c.Status == 6)
                        {
                            worksheet.Cells["Q" + row].Value = "Close";
                            row6++;
                        }
                        else
                        {
                            worksheet.Cells["Q" + row].Value = "Other";
                        }
                        row++;
                    }
                    if (JobDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + JobDTO.Count + " Job.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Job.";
                    }

                    worksheet.Cells["A" + (row + 2)].Value = "Job Wait for approval: " + row0 + ".";
                    worksheet.Cells["A" + (row + 3)].Value = "Job Approve: " + row1 + ".";
                    worksheet.Cells["A" + (row + 4)].Value = "Job Pending: " + row2 + ".";
                    worksheet.Cells["A" + (row + 5)].Value = "Job Reject: " + row3 + ".";
                    worksheet.Cells["A" + (row + 6)].Value = "Job Complete: " + row4 + ".";
                    worksheet.Cells["A" + (row + 7)].Value = "Job Draft: " + row5 + ".";
                    worksheet.Cells["A" + (row + 8)].Value = "Job Close: " + row6 + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Job.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [HttpGet]
        [Route("Staff")]
        public async Task<IActionResult> ExportDataStaff()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Staff");

                    // Đặt dữ liệu vào tệp Excel
                    worksheet.Cells["A1"].Value = "Staff id";
                    worksheet.Cells["B1"].Value = "Email";
                    worksheet.Cells["C1"].Value = "Username";
                    worksheet.Cells["D1"].Value = "PhoneNumber";
                    worksheet.Cells["E1"].Value = "Address";
                    worksheet.Cells["F1"].Value = "FullName";
                    worksheet.Cells["G1"].Value = "Avatar";
                    worksheet.Cells["H1"].Value = "Gender";
                    worksheet.Cells["I1"].Value = "Dob";
                    worksheet.Cells["J1"].Value = "Status";
                    worksheet.Cells["K1"].Value = "TypeManagers List";


                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 50;
                    worksheet.Column(6).Width = 20;
                    worksheet.Column(7).Width = 100;
                    worksheet.Column(8).Width = 15;
                    worksheet.Column(9).Width = 30;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(11).Width = 100;
                    List<User> Staffs = new List<User>();
                    List<StaffDTO> StaffDTO = new List<StaffDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            Staffs = context.Users
                                .Include(c => c.TypeManagerUsers)
                                .ThenInclude(c => c.TypeManager)
                                .Where(s => s.Role == 1).ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (Staffs != null)
                    {
                        StaffDTO = Staffs.Select(e => _mapper.Map<StaffDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int rowActive = 0;
                    int rowInActive = 0;
                    foreach (StaffDTO c in StaffDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.Uid;
                        worksheet.Cells["B" + row].Value = c.Uemail;
                        worksheet.Cells["C" + row].Value = c.Username;
                        worksheet.Cells["D" + row].Value = c.PhoneNumber;
                        worksheet.Cells["E" + row].Value = c.Address;
                        worksheet.Cells["F" + row].Value = c.FullName;
                        worksheet.Cells["G" + row].Value = c.Avatar;
                        if (c.Gender)
                        {
                            worksheet.Cells["H" + row].Value = "Male";
                        }
                        else
                        {
                            worksheet.Cells["H" + row].Value = "FeMale ";
                        }

                        if (c.Dob == null)
                        {
                            worksheet.Cells["I" + row].Value = c.Dob;
                        }
                        else
                        {
                            DateTime DobTime = DateTime.SpecifyKind((DateTime)c.Dob, DateTimeKind.Utc);
                            worksheet.Cells["I" + row].Value = DobTime;
                            worksheet.Cells["I" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }
                        if (c.Status == 0)
                        {
                            worksheet.Cells["J" + row].Value = "InActive";
                            rowInActive++;
                        }
                        else
                        {
                            worksheet.Cells["J" + row].Value = "Active ";
                            rowActive++;
                        }
                        string TypeManagers = "";
                        foreach (TypeManagerDTO item in c.TypeManagers)
                        {
                            TypeManagers += item.TypeManagerName+", ";
                        }
                        worksheet.Cells["K" + row].Value = TypeManagers;
                        row++;
                    }
                    if (StaffDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + StaffDTO.Count + " Staff.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Staff.";
                    }

                    worksheet.Cells["A" + (row + 2)].Value = "Staff Active: " + rowActive + ".";
                    worksheet.Cells["A" + (row + 3)].Value = "Staff InActive: " + rowInActive + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Staff.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> ExportDataReport()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");
                    // Đặt dữ liệu vào tệp Excel
                    worksheet.Cells["A1"].Value = "Report Id";
                    worksheet.Cells["B1"].Value = "Customer Name";
                    worksheet.Cells["C1"].Value = "CustomerEmail";
                    worksheet.Cells["D1"].Value = "Employee Name";
                    worksheet.Cells["E1"].Value = "Content";
                    worksheet.Cells["F1"].Value = "Note";
                    worksheet.Cells["G1"].Value = "Timestamp";
                    worksheet.Cells["H1"].Value = "TimeFeedback";
                    worksheet.Cells["I1"].Value = "Feedback";
                    worksheet.Cells["J1"].Value = "Status";
                    worksheet.Cells["K1"].Value = "ReportImages";


                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 100;
                    worksheet.Column(6).Width = 100;
                    worksheet.Column(7).Width = 30;
                    worksheet.Column(8).Width = 30;
                    worksheet.Column(9).Width = 100;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(11).Width = 100;
                    List<Report> Reports = new List<Report>();
                    List<ReportDTO> ReportDTO = new List<ReportDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            Reports = context.Reports.Include(x => x.ReportImages).Include(x => x.Customer).Include(x => x.User).ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (Reports != null)
                    {
                        ReportDTO = Reports.Select(e => _mapper.Map<ReportDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int rowActive = 0;
                    int rowInActive = 0;
                    foreach (ReportDTO c in ReportDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.Uid;
                        worksheet.Cells["B" + row].Value = c.CustomerName;
                        worksheet.Cells["C" + row].Value = c.CustomerEmail;
                        worksheet.Cells["D" + row].Value = c.EmployeeName;
                        worksheet.Cells["E" + row].Value = c.Content;
                        worksheet.Cells["F" + row].Value = c.Note;

                        if (c.Timestamp == null)
                        {
                            worksheet.Cells["G" + row].Value = c.Timestamp;
                        }
                        else
                        {
                            DateTime TimestampTime = DateTime.SpecifyKind((DateTime)c.Timestamp, DateTimeKind.Utc);
                            worksheet.Cells["G" + row].Value = TimestampTime;
                            worksheet.Cells["G" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }
                        if (c.TimeFeedback == null)
                        {
                            worksheet.Cells["H" + row].Value = c.TimeFeedback;

                        }
                        else
                        {
                            DateTime TimeFeedbackTime = DateTime.SpecifyKind((DateTime)c.TimeFeedback, DateTimeKind.Utc);
                            worksheet.Cells["H" + row].Value = TimeFeedbackTime;
                            worksheet.Cells["H" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }
                        worksheet.Cells["I" + row].Value = c.Feedback;
                        if (c.Status == 0)
                        {
                            worksheet.Cells["J" + row].Value = "UnSend";
                            rowInActive++;
                        }
                        else
                        {
                            worksheet.Cells["J" + row].Value = "Send";
                            rowActive++;
                        }
                        string ReportImages = "";
                        foreach (ReportImageDTO item in c.ReportImages)
                        {
                            ReportImages += item.Image + ", ";
                        }
                        worksheet.Cells["K" + row].Value = ReportImages;
                        row++;
                    }
                    if (ReportDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + ReportDTO.Count + " Report.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Report.";
                    }

                    worksheet.Cells["A" + (row + 2)].Value = "Report Send: " + rowActive + ".";
                    worksheet.Cells["A" + (row + 3)].Value = "Report UnSend: " + rowInActive + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpGet]
        [Route("Customer")]
        public async Task<IActionResult> ExportDataCustommer()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer");

                    // Đặt dữ liệu vào tệp Excel
                    worksheet.Cells["A1"].Value = "Customer id";
                    worksheet.Cells["B1"].Value = "Email";
                    worksheet.Cells["C1"].Value = "Role";
                    worksheet.Cells["D1"].Value = "PhoneNumber";
                    worksheet.Cells["E1"].Value = "FullName";
                    worksheet.Cells["F1"].Value = "Address";
                    worksheet.Cells["G1"].Value = "Descrition";
                    worksheet.Cells["H1"].Value = "Avatar";
                    worksheet.Cells["I1"].Value = "Dob";
                    worksheet.Cells["J1"].Value = "CreateDate";
                    worksheet.Cells["K1"].Value = "Epoint";
                    worksheet.Cells["L1"].Value = "Gender";
                    worksheet.Cells["M1"].Value = "Voting";
                    worksheet.Cells["N1"].Value = "Status";

                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 25;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(6).Width = 30;
                    worksheet.Column(7).Width = 100;
                    worksheet.Column(8).Width = 100;
                    worksheet.Column(9).Width = 25;
                    worksheet.Column(10).Width = 25;
                    worksheet.Column(11).Width = 10;
                    worksheet.Column(12).Width = 10;
                    worksheet.Column(13).Width = 10;
                    worksheet.Column(14).Width = 20;
                    List<Customer> cus = new List<Customer>();
                    List<CustomerDTO> cusDTO = new List<CustomerDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            cus = context.Customers.ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (cus != null)
                    {
                        cusDTO = cus.Select(e => _mapper.Map<CustomerDTO>(e)).ToList();
                    }

                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int row = 2;
                    int rowActive = 0;
                    int rowInActive = 0;
                    int rowAssigner = 0;
                    int rowSeeker = 0;
                    foreach (CustomerDTO c in cusDTO)
                    {
                        worksheet.Cells["A" + row].Value = c.Cid;
                        worksheet.Cells["B" + row].Value = c.Cemail;
                        if (c.Role == 1)
                        {
                            worksheet.Cells["C" + row].Value = "Job Assigner";
                            rowAssigner++;
                        }
                        else
                        {
                            worksheet.Cells["C" + row].Value = "Job Seeker";
                            rowSeeker++;
                        }
                        worksheet.Cells["D" + row].Value = c.PhoneNumber;
                        worksheet.Cells["E" + row].Value = c.FullName;
                        worksheet.Cells["F" + row].Value = c.Address;
                        worksheet.Cells["G" + row].Value = c.Descrition;
                        worksheet.Cells["H" + row].Value = c.Avatar;

                        if (c.Dob == null)
                        {
                            worksheet.Cells["I" + row].Value = c.Dob;
                        }
                        else
                        {
                            DateTime DobTime = DateTime.SpecifyKind((DateTime)c.Dob, DateTimeKind.Utc);
                            worksheet.Cells["I" + row].Value = DobTime;
                            worksheet.Cells["I" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }

                        if (c.CreateDate == null)
                        {
                            worksheet.Cells["J" + row].Value = c.CreateDate;
                        }
                        else
                        {
                            DateTime createDateTime = DateTime.SpecifyKind((DateTime)c.CreateDate, DateTimeKind.Utc);
                            worksheet.Cells["J" + row].Value = createDateTime;
                            worksheet.Cells["J" + row].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                        }

                        worksheet.Cells["K" + row].Value = c.Epoint;

                        if (c.Gender)
                        {
                            worksheet.Cells["L" + row].Value = "Male";
                        }
                        else
                        {
                            worksheet.Cells["L" + row].Value = "Female";
                        }
                        worksheet.Cells["M" + row].Value = c.Voting;
                        if (c.Status == 1)
                        {
                            worksheet.Cells["N" + row].Value = "Active";
                            rowActive++;
                        }
                        else
                        {
                            worksheet.Cells["N" + row].Value = "InActive";
                            rowInActive++;
                        }
                        row++;
                    }
                    if (cusDTO != null)
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: " + cusDTO.Count + " Customer.";
                    }
                    else
                    {
                        worksheet.Cells["A" + (row + 1)].Value = "Total: 0 Customer.";
                    }

                    worksheet.Cells["A" + (row + 2)].Value = "Customer Active: " + rowActive + ".";
                    worksheet.Cells["A" + (row + 3)].Value = "Customer InActive: " + rowInActive + ".";
                    worksheet.Cells["A" + (row + 4)].Value = "Job Assigner: " + rowAssigner + ".";
                    worksheet.Cells["A" + (row + 5)].Value = "Job Seeker: " + rowSeeker + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customer.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [HttpGet]
        [Route("Transaction")]
        public async Task<IActionResult> ExportDataTransaction()
        {
            try
            {
                // Set the license context before creating the ExcelPackage instance
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                using (var package = new ExcelPackage())
                {
                    List<Transaction> Transactions = new List<Transaction>();
                    List<TransactionDTO> TransactionDTO = new List<TransactionDTO>();
                    try
                    {
                        using (var context = new Viecvat247DBcontext())
                        {
                            Transactions = context.Transactions
                                .Include(j => j.Customer)
                                .Include(j => j.Receiver)
                                .Include(j => j.Job).ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    if (Transactions != null)
                    {
                        TransactionDTO = Transactions.Select(e => _mapper.Map<TransactionDTO>(e)).ToList();
                    }
                    // Tạo một bảng trong tệp Excel
                    ExcelWorksheet deposite = package.Workbook.Worksheets.Add("Deposite");
                    ExcelWorksheet withdraw = package.Workbook.Worksheets.Add("Withdraw");
                    ExcelWorksheet money_create_post = package.Workbook.Worksheets.Add("Money Create Post");
                    ExcelWorksheet money_refund = package.Workbook.Worksheets.Add("Money Refund");

                    // Đặt dữ liệu vào tệp Excel
                    //deposite
                    deposite.Cells["A1"].Value = "Transaction Id";
                    deposite.Cells["B1"].Value = "Customer Name";
                    deposite.Cells["C1"].Value = "Receiver Name";
                    deposite.Cells["D1"].Value = "JobName";
                    deposite.Cells["E1"].Value = "Epoint";
                    deposite.Cells["F1"].Value = "Detail";
                    deposite.Cells["G1"].Value = "Paymentdate";
                    deposite.Cells["H1"].Value = "Note";
                    deposite.Cells["I1"].Value = "TransactionType";
                    deposite.Cells["J1"].Value = "BankCode";
                    deposite.Cells["K1"].Value = "OldBalance";
                    deposite.Cells["L1"].Value = "NewBalance";
                    deposite.Cells["M1"].Value = "Status";

                    deposite.Column(1).Width = 15;
                    deposite.Column(2).Width = 20;
                    deposite.Column(3).Width = 20;
                    deposite.Column(4).Width = 20;
                    deposite.Column(5).Width = 10;
                    deposite.Column(6).Width = 100;
                    deposite.Column(7).Width = 30;
                    deposite.Column(8).Width = 100;
                    deposite.Column(9).Width = 15;
                    deposite.Column(10).Width = 30;
                    deposite.Column(11).Width = 15;
                    deposite.Column(12).Width = 15;
                    deposite.Column(13).Width = 20;

                    //withdraw
                    withdraw.Cells["A1"].Value = "Transaction Id";
                    withdraw.Cells["B1"].Value = "Customer Name";
                    withdraw.Cells["C1"].Value = "Receiver Name";
                    withdraw.Cells["D1"].Value = "JobName";
                    withdraw.Cells["E1"].Value = "Epoint";
                    withdraw.Cells["F1"].Value = "Detail";
                    withdraw.Cells["G1"].Value = "Paymentdate";
                    withdraw.Cells["H1"].Value = "Note";
                    withdraw.Cells["I1"].Value = "TransactionType";
                    withdraw.Cells["J1"].Value = "BankCode";
                    withdraw.Cells["K1"].Value = "OldBalance";
                    withdraw.Cells["L1"].Value = "NewBalance";
                    withdraw.Cells["M1"].Value = "Status";

                    withdraw.Column(1).Width = 15;
                    withdraw.Column(2).Width = 20;
                    withdraw.Column(3).Width = 20;
                    withdraw.Column(4).Width = 20;
                    withdraw.Column(5).Width = 10;
                    withdraw.Column(6).Width = 100;
                    withdraw.Column(7).Width = 30;
                    withdraw.Column(8).Width = 100;
                    withdraw.Column(9).Width = 15;
                    withdraw.Column(10).Width = 30;
                    withdraw.Column(11).Width = 15;
                    withdraw.Column(12).Width = 15;
                    withdraw.Column(13).Width = 20;

                    //money_create_post
                    money_create_post.Cells["A1"].Value = "Transaction Id";
                    money_create_post.Cells["B1"].Value = "Customer Name";
                    money_create_post.Cells["C1"].Value = "Receiver Name";
                    money_create_post.Cells["D1"].Value = "JobName";
                    money_create_post.Cells["E1"].Value = "Epoint";
                    money_create_post.Cells["F1"].Value = "Detail";
                    money_create_post.Cells["G1"].Value = "Paymentdate";
                    money_create_post.Cells["H1"].Value = "Note";
                    money_create_post.Cells["I1"].Value = "TransactionType";
                    money_create_post.Cells["J1"].Value = "BankCode";
                    money_create_post.Cells["K1"].Value = "OldBalance";
                    money_create_post.Cells["L1"].Value = "NewBalance";
                    money_create_post.Cells["M1"].Value = "Status";

                    money_create_post.Column(1).Width = 15;
                    money_create_post.Column(2).Width = 20;
                    money_create_post.Column(3).Width = 20;
                    money_create_post.Column(4).Width = 20;
                    money_create_post.Column(5).Width = 10;
                    money_create_post.Column(6).Width = 100;
                    money_create_post.Column(7).Width = 30;
                    money_create_post.Column(8).Width = 100;
                    money_create_post.Column(9).Width = 15;
                    money_create_post.Column(10).Width = 30;
                    money_create_post.Column(11).Width = 15;
                    money_create_post.Column(12).Width = 15;
                    money_create_post.Column(13).Width = 20;

                    //money_refund
                    money_refund.Cells["A1"].Value = "Transaction Id";
                    money_refund.Cells["B1"].Value = "Customer Name";
                    money_refund.Cells["C1"].Value = "Receiver Name";
                    money_refund.Cells["D1"].Value = "JobName";
                    money_refund.Cells["E1"].Value = "Epoint";
                    money_refund.Cells["F1"].Value = "Detail";
                    money_refund.Cells["G1"].Value = "Paymentdate";
                    money_refund.Cells["H1"].Value = "Note";
                    money_refund.Cells["I1"].Value = "TransactionType";
                    money_refund.Cells["J1"].Value = "BankCode";
                    money_refund.Cells["K1"].Value = "OldBalance";
                    money_refund.Cells["L1"].Value = "NewBalance";
                    money_refund.Cells["M1"].Value = "Status";

                    money_refund.Column(1).Width = 15;
                    money_refund.Column(2).Width = 20;
                    money_refund.Column(3).Width = 20;
                    money_refund.Column(4).Width = 20;
                    money_refund.Column(5).Width = 10;
                    money_refund.Column(6).Width = 100;
                    money_refund.Column(7).Width = 30;
                    money_refund.Column(8).Width = 100;
                    money_refund.Column(9).Width = 15;
                    money_refund.Column(10).Width = 30;
                    money_refund.Column(11).Width = 15;
                    money_refund.Column(12).Width = 15;
                    money_refund.Column(13).Width = 20;
                    // Bắt đầu từ hàng 2 để viết dữ liệu
                    int rowDeposite = 2;
                    int rowWithdraw = 2;
                    int rowMoney_create_post = 2;
                    int rowMoney_refund = 2;

                    long? totalDeposite = 0;
                    long? totalWithdraw = 0;
                    long? totalMoney_create_post = 0;
                    long? totalMoney_refund = 0;

                    int NumberDeposite = 0;
                    int NumberWithdraw = 0;
                    int NumberMoney_create_post = 0;
                    int NumberMoney_refund = 0;

                    foreach (TransactionDTO c in TransactionDTO)
                    {
                        if (c.TransactionType == 1)
                        {
                            totalDeposite += c.Epoint;
                            NumberDeposite++;
                            deposite.Cells["A" + rowDeposite].Value = c.TransactionId;
                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                deposite.Cells["B" + rowDeposite].Value = "Hệ Thống";
                            }
                            else
                            {
                                deposite.Cells["B" + rowDeposite].Value = c.CustomerName;
                            }

                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                deposite.Cells["C" + rowDeposite].Value = "Hệ Thống";
                            }
                            else
                            {
                                deposite.Cells["C" + rowDeposite].Value = c.ReceiverName;
                            }
                            deposite.Cells["D" + rowDeposite].Value = c.JobName;
                            deposite.Cells["E" + rowDeposite].Value = c.Epoint;
                            deposite.Cells["F" + rowDeposite].Value = c.Detail;
                            if (c.Paymentdate == null)
                            {
                                deposite.Cells["G" + rowDeposite].Value = c.Paymentdate;
                            }
                            else
                            {
                                DateTime PaymentdateTime = DateTime.SpecifyKind((DateTime)c.Paymentdate, DateTimeKind.Utc);
                                deposite.Cells["G" + rowDeposite].Value = PaymentdateTime;
                                deposite.Cells["G" + rowDeposite].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                            }
                            deposite.Cells["H" + rowDeposite].Value = c.Note;
                            deposite.Cells["I" + rowDeposite].Value = "Deposite";
                            deposite.Cells["J" + rowDeposite].Value = c.BankCode;
                            deposite.Cells["K" + rowDeposite].Value = c.OldBalance;
                            deposite.Cells["L" + rowDeposite].Value = c.NewBalance;
                            deposite.Cells["M" + rowDeposite].Value = c.Status;
                            rowDeposite++;
                        }
                        else if (c.TransactionType == 2)
                        {
                            totalWithdraw += c.Epoint;
                            NumberWithdraw++;
                            withdraw.Cells["A" + rowWithdraw].Value = c.TransactionId;
                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                withdraw.Cells["B" + rowWithdraw].Value = "Hệ Thống";
                            }
                            else
                            {
                                withdraw.Cells["B" + rowWithdraw].Value = c.CustomerName;
                            }

                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                withdraw.Cells["C" + rowWithdraw].Value = "Hệ Thống";
                            }
                            else
                            {
                                withdraw.Cells["C" + rowWithdraw].Value = c.ReceiverName;
                            }
                            withdraw.Cells["D" + rowWithdraw].Value = c.JobName;
                            withdraw.Cells["E" + rowWithdraw].Value = c.Epoint;
                            withdraw.Cells["F" + rowWithdraw].Value = c.Detail;
                            if (c.Paymentdate == null)
                            {
                                withdraw.Cells["G" + rowWithdraw].Value = c.Paymentdate;
                            }
                            else
                            {
                                DateTime PaymentdateTime = DateTime.SpecifyKind((DateTime)c.Paymentdate, DateTimeKind.Utc);
                                withdraw.Cells["G" + rowWithdraw].Value = PaymentdateTime;
                                withdraw.Cells["G" + rowWithdraw].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                            }
                            withdraw.Cells["H" + rowWithdraw].Value = c.Note;
                            withdraw.Cells["I" + rowWithdraw].Value = "Withdraw";
                            withdraw.Cells["J" + rowWithdraw].Value = c.BankCode;
                            withdraw.Cells["K" + rowWithdraw].Value = c.OldBalance;
                            withdraw.Cells["L" + rowWithdraw].Value = c.NewBalance;
                            withdraw.Cells["M" + rowWithdraw].Value = c.Status;
                            rowWithdraw++;
                        }
                        else if(c.TransactionType == 3)
                        {
                            totalMoney_create_post += c.Epoint;
                            NumberMoney_create_post++;
                            money_create_post.Cells["A" + rowMoney_create_post].Value = c.TransactionId;
                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                money_create_post.Cells["B" + rowMoney_create_post].Value = "Hệ Thống";
                            }
                            else
                            {
                                money_create_post.Cells["B" + rowMoney_create_post].Value = c.CustomerName;
                            }

                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                money_create_post.Cells["C" + rowMoney_create_post].Value = "Hệ Thống";
                            }
                            else
                            {
                                money_create_post.Cells["C" + rowMoney_create_post].Value = c.ReceiverName;
                            }
                            money_create_post.Cells["D" + rowMoney_create_post].Value = c.JobName;
                            money_create_post.Cells["E" + rowMoney_create_post].Value = c.Epoint;
                            money_create_post.Cells["F" + rowMoney_create_post].Value = c.Detail;
                            if (c.Paymentdate == null)
                            {
                                money_create_post.Cells["G" + rowMoney_create_post].Value = c.Paymentdate;
                            }
                            else
                            {
                                DateTime PaymentdateTime = DateTime.SpecifyKind((DateTime)c.Paymentdate, DateTimeKind.Utc);
                                money_create_post.Cells["G" + rowMoney_create_post].Value = PaymentdateTime;
                                money_create_post.Cells["G" + rowMoney_create_post].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                            }
                            money_create_post.Cells["H" + rowMoney_create_post].Value = c.Note;
                            money_create_post.Cells["I" + rowMoney_create_post].Value = "Money Create Post";
                            money_create_post.Cells["J" + rowMoney_create_post].Value = c.BankCode;
                            money_create_post.Cells["K" + rowMoney_create_post].Value = c.OldBalance;
                            money_create_post.Cells["L" + rowMoney_create_post].Value = c.NewBalance;
                            money_create_post.Cells["M" + rowMoney_create_post].Value = c.Status;
                            rowMoney_create_post++;
                        }
                        else if(c.TransactionType == 4)
                        {
                            totalMoney_refund += c.Epoint;
                            NumberMoney_refund++;
                            money_refund.Cells["A" + rowMoney_refund].Value = c.TransactionId;
                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                money_refund.Cells["B" + rowMoney_refund].Value = "Hệ Thống";
                            }
                            else
                            {
                                money_refund.Cells["B" + rowMoney_refund].Value = c.CustomerName;
                            }

                            if (!string.IsNullOrWhiteSpace(c.CustomerName))
                            {
                                money_refund.Cells["C" + rowMoney_refund].Value = "Hệ Thống";
                            }
                            else
                            {
                                money_refund.Cells["C" + rowMoney_refund].Value = c.ReceiverName;
                            }
                            money_refund.Cells["D" + rowMoney_refund].Value = c.JobName;
                            money_refund.Cells["E" + rowMoney_refund].Value = c.Epoint;
                            money_refund.Cells["F" + rowMoney_refund].Value = c.Detail;
                            if (c.Paymentdate == null)
                            {
                                money_refund.Cells["G" + rowMoney_refund].Value = c.Paymentdate;
                            }
                            else
                            {
                                DateTime PaymentdateTime = DateTime.SpecifyKind((DateTime)c.Paymentdate, DateTimeKind.Utc);
                                money_refund.Cells["G" + rowMoney_refund].Value = PaymentdateTime;
                                money_refund.Cells["G" + rowMoney_refund].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                            }
                            money_refund.Cells["H" + rowMoney_refund].Value = c.Note;
                            money_refund.Cells["I" + rowMoney_refund].Value = "Money Refund";
                            money_refund.Cells["J" + rowMoney_refund].Value = c.BankCode;
                            money_refund.Cells["K" + rowMoney_refund].Value = c.OldBalance;
                            money_refund.Cells["L" + rowMoney_refund].Value = c.NewBalance;
                            money_refund.Cells["M" + rowMoney_refund].Value = c.Status;
                            rowMoney_refund++;
                        }
                    }
                    deposite.Cells["A" + (rowDeposite + 1)].Value = "Total Transaction: " + NumberDeposite + ".";
                    deposite.Cells["A" + (rowDeposite + 2)].Value = "Total Goal: " + totalDeposite  + ".";

                    withdraw.Cells["A" + (rowWithdraw + 1)].Value = "Total Transaction: " +  NumberWithdraw + ".";
                    withdraw.Cells["A" + (rowWithdraw + 2)].Value = "Total Goal: " + totalWithdraw + ".";

                    money_create_post.Cells["A" + (rowMoney_create_post + 1)].Value = "Total Transaction: " + NumberMoney_create_post + ".";
                    money_create_post.Cells["A" + (rowMoney_create_post + 2)].Value = "Total Goal: " + totalMoney_create_post  + ".";

                    money_refund.Cells["A" + (rowMoney_refund + 1)].Value = "Total Transaction: " +  NumberMoney_refund + ".";
                    money_refund.Cells["A" + (rowMoney_refund + 2)].Value = "Total Goal: " + totalMoney_refund + ".";
                    // Save the Excel package to a memory stream
                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Position = 0;

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransactionHistory.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

    }

    
}
