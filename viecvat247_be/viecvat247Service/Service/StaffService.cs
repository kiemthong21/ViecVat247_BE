using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace viecvat247Service.Service
{
    public class StaffService : IStaffService
    {

        IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
        public User AddNewStaff(User user)
        => StaffDAO.CreateStaff(user);

        
        public void CreateTypeManagerUser(string[]? typeManagers, int id)
        => StaffDAO.CreateTypeManagerUser(typeManagers, id);

        public void DeleteTypeManagerUserbyUserId(int id)
        => StaffDAO.DeleteTypeManagerUserbyUserId(id);

        public List<TypeManager> GetAllTypeManager()
        => StaffDAO.GetAllTypeManager();

        public User GetStaffByEmailAndPhone(string? uemail, string? phoneNumber)
       => StaffDAO.GetStaffByEmailAndPhone(uemail, phoneNumber);

        public User GetStaffById(int id)
        => StaffDAO.GetStaffById(id);

        public PaginatedList<User> GetStaffs(string searchValue, int pageIndex, int pageSize, string orderBy)
        => StaffDAO.GetStaffs(searchValue, pageIndex, pageSize, orderBy);

        public List<User> GetStaffss(string searchValue, int pageIndex, int pageSize, string orderBy)
        => StaffDAO.GetStaffss(searchValue, pageIndex, pageSize, orderBy);

        public TypeManager GettypeManager(int id)
        => StaffDAO.GettypeManager(id);

        public void SendMailAddnewStaff(string mail, string password, string fullname)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(mail));
            email.Subject = "Chào mừng thành viên mới";
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
                                    <h1>Chào mừng: '" + fullname + @"'</h1>
                                    <p>Việc vặt 247 chào mừng bạn đã đến và tham gia vào đại gia đình này .</p>
                                    <p>Nếu có thắc mắc nào trước và trong quá trình tiếp quản công việc, anh/chị có thể liên hệ trực tiếp với tôi theo thông tin ở phần chữ ký email.
Một lần nữa, thay mặt công ty hân hoan chào đón anh/chị đến với ngôi nhà chung.</p>
<p>Mật khẩu để vào tài khoản của bạn là: <i>'" + password + @"' </i> </p>
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

        public void UpdateStaff(User user)
        => StaffDAO.UpdateStaff(user);
    }
}
