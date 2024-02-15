using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace viecvat247Service.Service
{
    public class ReportService : IReportService
    {
        IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        public Report AddReport(NewReportDTO report, int cid)
        => ReportDAO.AddReport(report, cid);

        public void DeleteReport(Report report)
        => ReportDAO.DeleteReport(report);

        public Report GetReport(int id)
        => ReportDAO.GetReportById(id);

        public PaginatedList<Report> GetReports(string searchValue, string status,
            DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string orderBy)
        => ReportDAO.GetReports(searchValue, status, startDate, endDate, pageIndex, pageSize, orderBy);

        public void UpdateReport(Report report)
        => ReportDAO.UpdateReport(report);


        public void SendFeedbackReport(Report report)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(report?.Customer?.Cemail));
            email.Subject = "Thư trả lời phản hồi.";
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
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Kính gửi <span style=""color: #333;"">' "+ report?.Customer?.FullName + @" '</span>.</h1>
					<p style=""margin: 0; margin-top: 20px;""> " + report?.Feedback + @"</p> </br>
                    <p style=""margin: 0; margin-top: 20px;""> Việc Vặt 247 trân thành cảm ơn bạn vì đã đóng góp và phản hồi cho công ty. Công ty sẽ cố gắng phát triển và tiếp thu ý kiến để giúp người dùng có trải nghiệm tốt nhất.</p>
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
    }
}
