using AutoMapper;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IAuthenService _authenService = new AuthenService();
        private readonly IOtherService _otherService = new OtherService();

        private readonly IMapper _mapper;

        public AuthenController(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        private string GenerateTokens(AccountLoginDTO account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var claims = new[]
            {
                new Claim("NameIdentifier", account.AccID.ToString()),
                new Claim("Name", account.Name),
                new Claim("Role", account.Role.ToString()),
                new Claim("ProfileStatus", account.ProfileStatus.ToString())
            };
            //var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: credentials);
            //return new JwtSecurityTokenHandler().WriteToken(token);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            // verify the the validation of JWT
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a new JWT by some info to spicify contend of JWT 
            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("Customer/Login")]
        public IActionResult CustomerLogin(LoginDTO login)
        {
            try
            {
                IActionResult response = Unauthorized();
                if (login != null)
                {
                    var account = new AccountLoginDTO();
                    var cus = _authenService.GetCustomerByLogin(login);
                    if (cus != null)
                    {
                        if (cus.Status == 0)
                        {
                            response = Ok(new { message = "account_not_confirm_mail" });
                        }
                        else if(cus.Status == 3)
                        {
                            response = Ok(new { message = "account_has_ban" });
                        }
                        else if(cus.Status == 1)
                        {
                            account.Avatar = cus.Avatar;
                            account.AccID = cus.Cid;
                            account.Name = cus.FullName;
                            account.Role = (int)cus.Role;
                            account.ProfileStatus = cus.FrofileStatus;
                            var token = GenerateTokens(account);
                            response = Ok(new { token = token, uid = cus.Cid, money = cus.Epoint, roleid = cus.Role, fullname = cus.FullName, avatar = cus.Avatar });
                        }
                        else
                        {
                            return BadRequest(new { error = "error" });
                        }
                    }
                    else
                    {
                        response = Ok(new { message = "email_password_wrong" });
                    }
                }
                return response;
            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }

        }



        [AllowAnonymous]
        [HttpPost("Staff/Login")]
        public IActionResult StaffLogin(LoginDTO login)
        {
            IActionResult response = Unauthorized();
            if (login != null)
            {
                var cus = _authenService.GetUserByLogin(login);
                
                if (cus != null)
                {
                    if(cus.Status == 0)
                    {
                        response = Ok(new { message = "account_has_ban" });
                    }
                    else
                    {
                        AccountLoginDTO account = _mapper.Map<AccountLoginDTO>(cus);
                        for (int i = 0; i < account.TypeManagers.Count; i++)
                        {
                            if (account.TypeManagers[i].Status == 0)
                            {
                                account.TypeManagers.Remove(account.TypeManagers[i]);
                                i--;
                            }
                        }
                        account.Avatar = cus.Avatar;
                        account.AccID = cus.Uid;
                        account.Name = cus.FullName;
                        account.Role = (int)cus.Role;
                        var token = GenerateTokens(account);
                        response = Ok(new { token = token, uid = cus.Uid, roleid = cus.Role, fullname = cus.FullName, avatar = cus.Avatar, typeManager = account.TypeManagers });
                    }
                    
                }
                else
                {
                    response = Ok(new { message = "email_password_wrong" });
                }
            }
            return response;
        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterDTO register)
        {
            if (register == null)
            {

                return Ok(new { message = "register_is_null" });
            }
            if (_authenService.GetCustomerByEmail(register.Email) != null)
            {
                return Ok(new { message = "email_has_been_registered" });

            }
            else
            {
                try
                {
                    Customer cus = new Customer();
                    cus.Epoint = 0;
                    cus.Cemail = register.Email;
                    byte[] bytescode = Encoding.UTF8.GetBytes(register.Password);
                    cus.Password = Convert.ToHexString(SHA256.HashData(bytescode));
                    cus.FullName = register.Fullname;
                    cus.CreateDate = DateTime.Now;
                    cus.VerifyCode = _otherService.GenerateRandomString(16);
                    cus.Gender = true;
                    cus.Role = register.roleId;
                    cus.Status = 0;
                    cus.FrofileStatus = 0;
                    cus.Dob = DateTime.Now;
                    cus.Type = register.roleId;
                    var cusR = _authenService.Register(cus);
                    _authenService.SendMail(cusR.Cid, register.Email, cus.VerifyCode);
                    return Ok(new { message = "register_successful" });
                }
                catch (Exception)
                {
                    return BadRequest(new { error = "error" });
                }
            }

        }


        [AllowAnonymous]
        [HttpGet("ConfirmMail/{id}/{code}")]
        public IActionResult ConfirmMail(int id, string code)
        {
            var cus = _authenService.GetCustomerToConfirmMail(id, code);
            if (cus == null)
            {
                return Ok(new { message = "email_confirmation_failed" });
            }
            else
            {
                _authenService.ConfirmMailDone(id);
                return Ok(new { message = "email_confirmation_complete" });
            }
        }

    }
}
