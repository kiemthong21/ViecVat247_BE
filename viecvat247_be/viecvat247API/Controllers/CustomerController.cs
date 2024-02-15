using AutoMapper;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using viecvat247API.Hubs;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService = new CustomerService();
        private IAuthenService _authenService = new AuthenService();
        private IOtherService _otherService = new OtherService();
        private ISkillService _skillService = new SkillService();
        private IReportService _reportService = new ReportService();
        private INotificationService _notificationService = new NotificationService();

        private IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;


        public CustomerController(IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [AllowAnonymous]
        [HttpGet("Encrypt/{mail}")]
        public IActionResult Ecrypt(string mail)
        {
            return Ok(_otherService.Encrypt(mail));
        }

        [AllowAnonymous]
        [HttpGet("Decrypy/{mail}")]
        public IActionResult DeEcrypt(string mail)
        {
            return Ok(_otherService.Decrypt(mail));
        }

        [Authorize]
        [HttpGet("UserProfile")]
        public IActionResult getUserProfile()
        {
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            var cus = _customerService.GetCustomerById(int.Parse(cid));
            CustomerProfileDTO CustomerProfileDTO = _mapper.Map<CustomerProfileDTO>(cus);
            if (CustomerProfileDTO.Skills != null)
            {
                CustomerProfileDTO.ListSkills = new List<int>();
                foreach (SkillDTO item in CustomerProfileDTO.Skills)
                {
                    CustomerProfileDTO.ListSkills.Add(item.SkillId);
                }
            }
            
            return Ok(CustomerProfileDTO);
        }

        [Authorize]
        [HttpPut("UserProfile")]
        public IActionResult EditProfile(CustomerEditProfileDTO customerEdit)
        {
            try
            {
                string[] listSkill = customerEdit.Skills;
                for (int i = 0; i < listSkill.Length; i++)
                {
                    if (_skillService.GetSkill(int.Parse(listSkill[i])) == null)
                    {
                        return Ok(new { message = "skill_not_found" });
                    }
                }
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                Customer cus = _customerService.GetCustomerById(Int16.Parse(cid.ToString()));
                Customer customer = _mapper.Map<Customer>(customerEdit);
                customer.Cid = cus.Cid;
                customer.Cemail = cus.Cemail;
                customer.Password = cus.Password;
                customer.Role = cus.Role;
                customer.CreateDate = cus.CreateDate;
                customer.UpdateDate = DateTime.Now;
                customer.Epoint = cus.Epoint;
                customer.Voting = cus.Voting;
                customer.Type = cus.Type;
                customer.VerifyCode = cus.VerifyCode;
                customer.FrofileStatus = 1;
                customer.Status = cus.Status;
                _customerService.UpdateCustomer(customer);
                _customerService.DeleteCustomerSkillbyCustomerId(customer.Cid);
                _customerService.CreateCustomerSkill(listSkill, customer.Cid);
                return Ok(new { message = "update_successful" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO cPass)
        {
            //var campusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            var cus = _customerService.GetCustomerById(Int16.Parse(cid.ToString()));
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
                _customerService.UpdateCustomer(cus);
            }

            return Ok(new { message = "update_successful" });
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPasswordRequest(string email)
        {
            var cus = _authenService.GetCustomerByEmail(email);
            if (cus == null)
            {
                return Ok(new { message = "not_found" });
            }
            else
            {
                _customerService.SendMailForgotPassword(email, cus.VerifyCode, cus.Cid);
            }

            return Ok(new { message = "check_mail" });
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword/{mail}/{code}")]
        public IActionResult ForgotPasswordScreen(string email, string code)
        {
            string mail = _otherService.Decrypt(email);
            var cus = _authenService.GetCustomerByEmail(mail);
            if (code != cus.VerifyCode || cus == null)
            {
                return BadRequest(new { error = "error" });
            }
            return Ok(new { mail = email, code = code });
        }


        [AllowAnonymous]
        [HttpPost("ForgotPassword/{id}/{code}")]
        public IActionResult ForgotPasswordScreen(string id, string code, ForgotPasswordRequestDTO pass)
        {
            var cus = _customerService.GetCustomerById(Int32.Parse(id));
            if (cus == null || code != cus.VerifyCode)
            {
                return BadRequest(new { error = "error" });
            }
            else
            {
                byte[] bytescode = Encoding.UTF8.GetBytes(pass.NewPassword);
                cus.Password = Convert.ToHexString(SHA256.HashData(bytescode));
                cus.VerifyCode = _otherService.GenerateRandomString(16);
                _customerService.UpdateCustomer(cus);
            }
            return Ok(new { message = "update_successful." });
        }

        [Authorize]
        [HttpPost("SendReport")]
        public IActionResult SendReport(NewReportDTO nReport)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                _reportService.AddReport(nReport, Int32.Parse(cid));
                return Ok(new { message = "send_successful." });
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
            
        }

        [AllowAnonymous]
        [HttpGet("GetProfileById/{id}")]
        public IActionResult GetProfileById(string id)
        {
            var cus = _customerService.GetCustomerById(Int32.Parse(id));
            CustomerProfileGetJobDTO CustomerProfileGetJobDTO = _mapper.Map<CustomerProfileGetJobDTO>(cus);
            if (CustomerProfileGetJobDTO.Skills != null)
            {
                CustomerProfileGetJobDTO.ListSkills = new List<int>();
                foreach (SkillDTO item in CustomerProfileGetJobDTO.Skills)
                {
                    CustomerProfileGetJobDTO.ListSkills.Add(item.SkillId);
                }
            }
            if (cus!=null)
            {
                return Ok(CustomerProfileGetJobDTO);
            }
            else
            {
                return Ok(new { message = "user_not_exist" });
            }
            
        }

        [Authorize]
        [HttpGet("GetInfoById/{cid}")]
        public IActionResult GetInfoById(string cid)
        {
            var uid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            var cus = _customerService.GetInfoById(Int32.Parse(cid), uid);
            if (cus != null)
            {
                return Ok(new {email = cus.Cemail,phone = cus.PhoneNumber});
            } 
            else
            {
                return Ok(new { message = "user_not_exist" });
            }

        }

        [HttpGet("GetNotification/{top}")]
        public async Task<IActionResult> GetNotification(int top = 10)
        {
            try {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (cid == null)
                {
                    return BadRequest(new { error = "error" });
                }
                var notifications = _notificationService.GetNotificationTop(Int32.Parse(cid), top);
                var list = _mapper.Map<List<NotificationDTO>>(notifications);
                return Ok(new {list = list});
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [AllowAnonymous]
        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetNotifications(int pageIndex = 1, int pageSize =10 )
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (cid == null)
                {
                    return BadRequest(new { error = "error" });
                }
                var notifications = _notificationService.GetNotifications(Int32.Parse(cid), pageIndex, pageSize);
                var list = _mapper.Map<List<NotificationDTO>>(notifications);
                return Ok(new { list = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("UpdateNotification")]
        public async Task<IActionResult> UpdateNotifications(NotificationUpdateDTO listNoti)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                if (listNoti == null)
                {
                    return Ok(new { message= "list_not_null" });
                }
                _notificationService.UpdateNotification(listNoti.ListNotificationID);
                await NotificationHub.UpdateNotification(_hubContext, cid);
                return Ok(new { message = "update_noti_success" });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [HttpPost("TestNotification")]
        public async Task<IActionResult> CreateNotification( string description)
        {
            // Logic để tạo sản phẩm
            try
            {
                var userId = "21"; // Thay thế bằng ID của người dùng cụ thể
               // await _hubContext.Clients.All.SendAsync("ReceiveNotification",  "");
                Notification notification = new Notification();
                notification.Timestamp = DateTime.Now;
                notification.CustomerId = 23;
                notification.Description = description;
                await NotificationHub.SendNotification(_hubContext, userId, description);
                return Ok("Thông báo đã được tạo thành công.");
            }catch (Exception ex)
            {
                return BadRequest(new { error = "error" });
            }
            
        }
    }
}
