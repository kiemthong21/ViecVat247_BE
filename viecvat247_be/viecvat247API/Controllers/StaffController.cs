using AutoMapper;
using BussinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers
{
    [Route("api/Staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaffService _staffService = new StaffService();
        private IAuthenService _authenService = new AuthenService();
        private IOtherService _otherService = new OtherService();
        private readonly IMapper _mapper;

        public StaffController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO cPass)
        {
            //var campusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _staffService.GetStaffById(Int16.Parse(cid.ToString()));
                byte[] opass = Encoding.UTF8.GetBytes(cPass.OldPassword);
                byte[] opassHashValue = SHA256.HashData(opass);
                bool checkPass = Convert.FromHexString(cus.Password).SequenceEqual(opassHashValue);
                if (checkPass == false)
                {
                    return Ok(new { message = "wrong_password" });
                }
                else
                {
                    byte[] bytescode = Encoding.UTF8.GetBytes(cPass.NewPassword);
                    cus.Password = Convert.ToHexString(SHA256.HashData(bytescode));
                    _staffService.UpdateStaff(cus);
                }
                return Ok(new { message = "update_successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("Profile")]
        public IActionResult GetProfileStaff()
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var staff = _staffService.GetStaffById(Int16.Parse(cid.ToString()));
                return Ok(_mapper.Map<StaffDTO>(staff));
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("EditProfile")]
        public IActionResult EditProfileStaff(StaffUpdateDTO staffDTO)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var staff = _staffService.GetStaffById(Int16.Parse(cid.ToString()));
                staff.Avatar = staffDTO.Avatar;
                staff.PhoneNumber = staffDTO.PhoneNumber;
                staff.FullName = staffDTO.FullName;
                staff.Gender = staffDTO.Gender;
                staff.Address = staffDTO.Address;
                staff.Dob = staffDTO.Dob;
                _staffService.UpdateStaff(staff);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


    }
}
