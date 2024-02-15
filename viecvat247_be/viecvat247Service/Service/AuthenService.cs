using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace viecvat247Service.Service
{
    public class AuthenService : IAuthenService
    {
        //private readonly string EmailHost = "smtp.gmail.com";
        //private readonly int EmailPort = 587;
        //private readonly string EmailClientID = "viecvat247@gmail.com";
        //private readonly string EmailPassword = "rfns tfhv wuzy cjpn";

        IConfiguration config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
        public void ConfirmMailDone(int id)
        => AuthenDAO.ConfirmMail(id);

        public Customer GetCustomerByLogin(LoginDTO login)
        => AuthenDAO.GetCustomerByLogin(login);

        public Customer GetCustomerToConfirmMail(int id, string code)
        => AuthenDAO.GetCustomerConfirmMail(code, id);

        public User GetUserByLogin(LoginDTO login)
        => AuthenDAO.GetUserByLogin(login);

        public Customer Register(Customer cus)
        => AuthenDAO.CreateAccount(cus);

        public void SendMail(int id, string mail, string code)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(mail));
            string link = config.GetSection("Constants:ConfigLink").Value + "/ConfirmMail?id=" + id.ToString() + "&code=" + code;
            email.Subject = "[LOGIN INVITATION] - Đây là mail confirm";
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
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Hãy xác nhận email của bạn</h1>
					<p style=""margin: 0; margin-top: 20px;"">Nhấn vào nút bên dưới để xác nhận địa chỉ email của bạn. Nếu bạn không tạo tài khoản với, bạn có thể xóa email này một cách an toàn.</p>
                  </td>
                </tr>
                <tr>
                  <td align=""center"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                      <tr>
                        <td align=""center"" bgcolor=""#01B195"" style=""border-radius: 6px;"">
                          <a href='" + link + @"' target=""_blank"" style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, 
Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;"">Xác nhận email</a>
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

        public Customer GetCustomerByEmail(string email)
        => AuthenDAO.GetCustomerByEmail(email);
    }
}
