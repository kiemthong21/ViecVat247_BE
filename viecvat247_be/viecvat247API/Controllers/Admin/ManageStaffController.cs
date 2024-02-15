using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Admin
{
    [Route("api/Admin")]
    [ApiController]
    public class ManageStaffController : ControllerBase
    {
        private IStaffService _staffService = new StaffService();
        private IConfiguration _config;
        private IOtherService _otherService = new OtherService();
        private readonly IMapper _mapper;
        //public ManageStaffController(IMapper mapper, IConfiguration config, IStaffService staffService, IOtherService otherService)
        //{
        //    _mapper = mapper;
        //    _config = config;
        //    _staffService = staffService;
        //    _otherService = otherService;
        //}

        public ManageStaffController(IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
        }


        [Authorize]
        [HttpPost("AddNewStaff")]
        public IActionResult AddStaff(NewStaffDTO staff)
        {
            try
            {
                if (staff == null)
                {
                    return BadRequest(new { error = "error" });
                }
                User staffExist = _staffService.GetStaffByEmailAndPhone(staff.Uemail, staff.PhoneNumber);
                if (staffExist != null)
                {
                    return Ok(new { message = "account_exist" });
                }
                else
                {
                    string pass = _otherService.GenerateRandomString(8);
                    byte[] bytescode = Encoding.UTF8.GetBytes(pass);
                    User newStaff = _mapper.Map<User>(staff);
                    newStaff.Status = 1;
                    newStaff.Password = Convert.ToHexString(SHA256.HashData(bytescode));
                    newStaff.Avatar = _config["Constants:ConfigImage"];
                    newStaff.Role = 1;
                    newStaff = _staffService.AddNewStaff(newStaff);
                    _staffService.SendMailAddnewStaff(newStaff.Uemail, pass, newStaff.FullName);
                    return Ok(new { message = "add_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("GetStaffs")]
        public IActionResult GetStaffs([FromQuery] string? searchValue, string? orderBy,
            int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    BadRequest(new { error = "error" });
                }
                PaginatedList<User> staff = _staffService.GetStaffs(searchValue, pageIndex, pageSize, orderBy);
                List<User> members = _staffService.GetStaffss(searchValue, pageIndex, pageSize, orderBy);
                List<StaffDTO> staffs = _mapper.Map<List<StaffDTO>>(members);
                foreach (StaffDTO item in staffs)
                {
                    for (int i = 0; i < item.TypeManagers.Count; i++)
                    {
                        if (item.TypeManagers[i].Status == 0)
                        {
                            item.TypeManagers.Remove(item.TypeManagers[i]);
                            i--;
                        }
                    }
                }

                return Ok(new { totalItems = staff.Totalsize, total = staff.TotalPages, Staff = staffs });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [Authorize]
        [HttpGet("GetStaff/{id}")]
        public IActionResult GetStaffs(int id)
        {
            try
            {
                User staff = _staffService.GetStaffById(id);
                if (staff == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    StaffDTO staffDTO = _mapper.Map<StaffDTO>(staff);
                    for (int i = 0; i < staffDTO.TypeManagers.Count; i++)
                    {
                        if (staffDTO.TypeManagers[i].Status == 0)
                        {
                            staffDTO.TypeManagers.Remove(staffDTO.TypeManagers[i]);
                            i--;
                        }
                    }
                    staffDTO.ListTypeManagers = new List<int>();
                    foreach (TypeManagerDTO item in staffDTO.TypeManagers)
                    {
                        staffDTO.ListTypeManagers.Add(item.TypeManagerId);
                    }
                    return Ok(staffDTO);
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("UpdateStaff")]
        public IActionResult UpdateStaff(EditStaffDTO staffEdit)
        {
            try
            {
                string[] typeManagers = staffEdit.TypeManagers;
                foreach (string typeManager in typeManagers)
                {
                    try
                    {
                        if (_staffService.GettypeManager(int.Parse(typeManager)) == null)
                        {
                            return Ok(new { message = "typeManager_not_found" });
                        }
                    }
                    catch (Exception)
                    {
                        return Ok(new { message = "typeManager_not_found" });
                    }

                }
                User staff = _staffService.GetStaffById(staffEdit.Id);
                if (staff == null)
                {
                    return Ok(new { message = "staff_not_found" });
                }
                staff.Status = staffEdit.Status;
                _staffService.UpdateStaff(staff);
                
                _staffService.DeleteTypeManagerUserbyUserId(staffEdit.Id);
                _staffService.CreateTypeManagerUser(typeManagers, staffEdit.Id);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("GetAllTypeManager")]
        public IActionResult GetAllTypeManager()
        {
            try
            {
                List<TypeManager> typeManager = _staffService.GetAllTypeManager();
                return Ok(_mapper.Map<List<TypeManagerDTO>>(typeManager));
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


    }
}
