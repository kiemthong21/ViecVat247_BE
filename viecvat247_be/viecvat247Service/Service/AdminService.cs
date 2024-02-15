using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace viecvat247Service.Service
{
    public class AdminService : IAdminService
    {

        private readonly OtherService _otherService = new OtherService();

        IConfiguration config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        public void SendMail(string pass, string mail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(mail));
            email.Subject = "[Forgot Password INVITATION] - Đây là mail thông báo Reset Password";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = @"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <title>Invitation Letter</title>
                                <style>
                                    /* Basic CSS styles for the invitation */
                                    body {
                                        font-family: Arial, sans-serif;
                                        margin: 0;
                                        padding: 0;
                                        background-color: #f2f2f2;
                                    }

                                    .container {
                                        max-width: 600px;
                                        margin: 0 auto;
                                        padding: 20px;
                                        background-color: #ffffff;
                                        border: 1px solid #ccc;
                                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                                    }

                                    h1 {
                                        color: #FF6000;
                                    }

                                    p {
                                        margin-bottom: 15px;
                                    }

                                    /* Style for the login information section */
                                    .login-info {
                                        background-color: #f2f2f2;
                                        padding: 10px;
                                        border: 1px solid #ccc;
                                    }

                                    /* Style for the button */

                                    .login-button span {
                                        color: #fff;
                                    }
                                </style>
                            </head>
                            <body>
                                <div class=""container"">
                                    <h1>Việc Vặt 247</h1>
                                    <p>Mật khẩu của bạn đã được reset.</p>
                                    <p>Mật khẩu mới của bạn là: </p>
                                    <a href=''>'" + pass + @"'</a>
                                </div>
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
    }
}
