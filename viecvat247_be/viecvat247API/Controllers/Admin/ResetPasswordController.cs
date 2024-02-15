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
    public class ResetPasswordController : ControllerBase
    {
        private IStaffService _staffService = new StaffService();
        private IOtherService _otherService = new OtherService();
        private IAdminService _adminService = new AdminService();

        [Authorize]
        [HttpPost("ResetPassword/{id}")]
        public IActionResult ResetPassword(int id)
        {
            try
            {
                var staff = _staffService.GetStaffById(id);
                if (staff == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    string pass = _otherService.GenerateRandomString(8);
                    byte[] bytescode = Encoding.UTF8.GetBytes(pass);
                    staff.Password = Convert.ToHexString(SHA256.HashData(bytescode));
                    _staffService.UpdateStaff(staff);
                    _adminService.SendMail(pass, staff.Uemail);
                    return Ok(new { message = "reset_successful" });
                }
            }
            catch
            {
                return BadRequest(new { error = "error" });
            }
        }

    }
}
