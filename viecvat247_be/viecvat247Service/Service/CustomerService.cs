using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace viecvat247Service.Service
{
    public class CustomerService : ICustomerService
    {
        //private readonly string EmailHost = "smtp.gmail.com";
        //private readonly int EmailPort = 587;
        //private readonly string EmailClientID = "viecvat247@gmail.com";
        //private readonly string EmailPassword = "rfns tfhv wuzy cjpn";
        private readonly OtherService _otherService = new OtherService();

        IConfiguration config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        public CustomerService() { }
        public void ChangeForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int id)
        => CustomerDAO.GetCustomerByID(id);

        public void CreateCustomerSkill(string[]? listSkill, int cusId)
        => CustomerDAO.CreateCustomerSkill(listSkill, cusId);

        public void DeleteCustomerSkillbyCustomerId(int cusId)
        => CustomerDAO.DeleteCustomerSkillbyCustomerId(cusId);

        public PaginatedList<Customer> GetCustomers(string searchValue, int pageIndex, int pageSize, string orderBy, string role, string status)
        => CustomerDAO.GetCustomers(searchValue, pageIndex, pageSize, orderBy, role, status);

        public void SendMailForgotPassword(string mail, string code, int id)
        {
            //  string encryptDate = _otherService.Encrypt(DateTime.Now.ToString());

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(mail));
            string link = config.GetSection("Constants:ConfigLink").Value + "/resetpassword?code=" + code + "&mail=" + id;
            email.Subject = "[Forgot Password INVITATION] - Đây là mail xác nhận thay đổi mật khẩu";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = @"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"">
  <meta http-equiv=""x-ua-compatible"" content=""ie=edge"">
  <title>Email Confirmation</title>
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
  <style type=""text/css"">
  </style>
</head>
<body style=""background-color: #ffffff;margin-top: 100px;"">


  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
    <tr>
      <td align=""center"" bgcolor=""#ffffff"">

        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
          <tr>
            <td align=""center"" bgcolor=""#ffffff"" valign=""top"" style=""padding: 36px 24px;"">
              <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                <tr>
                  <td align=""center"" style=""font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;"">
                    <img src='https://res.cloudinary.com/dwcvx2iql/image/upload/v1703495994/lpelxu8bys0tivx1edck.png' alt='Logo' border='0' width='130px' style='display: block; width: 130px; max-width: 130px; min-width: 130px;'>
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Bạn bị quên mật khẩu.</h1>
					<p style=""margin: 0; margin-top: 20px;"">Nhấn vào nút bên dưới để chuyển đến trang thay đổi mật khẩu.</p>
                  </td>
                </tr>
                <tr>
                  <td align=""center"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                      <tr>
                        <td align=""center"" bgcolor=""#01B195"" style=""border-radius: 6px;"">
                          <a href='" + link + @"' target=""_blank"" style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, 
Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;"">Xác nhận quên mật khẩu</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td align=""center"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
                    <p style=""margin: 0;"">Nếu bạn gặp vấn đề, xin hãy liên hệ qua email của chúng tôi:</p>
                    <p style=""margin: 0; color: #1A82E2;"">viecvat247@gmail.com</p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>

      </td>
    </tr>
  </table>
</body>
</html>
                            
                            "
            };
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(config.GetSection("Constants:Host").Value, Int32.Parse(config.GetSection("Constants:Port").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(config.GetSection("Constants:ClientID").Value, config.GetSection("Constants:ClientSecret").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            };
        }

        public void UpdateCustomer(Customer customer)
        => CustomerDAO.UpdateCustomer(customer);

        public PaginatedList<Customer> GetCustomersApplyByJob(int jobid, int pageIndex, int pageSize, string orderBy)
        => CustomerDAO.GetCustomersApplyByJob(jobid, pageIndex, pageSize, orderBy);

        public Customer GetInfoById(int cid, string? uid)
        => CustomerDAO.GetCustomerByID(cid,uid);

    }
}
