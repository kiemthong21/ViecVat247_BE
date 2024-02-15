using BusinessObject;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace viecvat247Service.Service
{
    public class ApplicantService : IApplicantService 
    {
        IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

        public Application CheckApplication(int jobId, int cusId)
        => ApplicationDAO.GetApplication(jobId, cusId);

        public Application CreateApplication(Application application)
        => ApplicationDAO.AddNewApplication(application);

        public void DeleteApplication(Application application)
        => ApplicationDAO.DeletableApplication(application);

        public Application GetApplication(int id)
        => ApplicationDAO.GetApplication(id);

        public PaginatedList<Application> GetApplications(string status, string typejob, string search, string jobCategory, string order, int pageIndex, int pageSize, int appID)
            => ApplicationDAO.GetApplications(status, typejob, search, jobCategory ,order, pageIndex, pageSize, appID);

        public int CountApplySuccessByJob(int jobId)
        => ApplicationDAO.CountAllApplicationsSuccessByJob(jobId);

        public PaginatedList<Application> GetCustomersApplyByJob(int jobid, string searchValue, int pageIndex, int pageSize, string orderBy, string? status)
        => ApplicationDAO.GetCustomersApplyByJob(jobid, searchValue, pageIndex, pageSize, orderBy, status);

        public void SendMailApply(Job job)
        {
            try
            {
                   string text = "Công việc " + job.Title + " đã có người ứng tuyển. Xin vui lòng truy cập vào trang web để biết thêm thông tin người ứng tuyển."; ;
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
                email.To.Add(MailboxAddress.Parse(job.JobAssigner.Cemail));
                email.Subject = "[Thông báo] - Đã có người đăng kí apply vào công việc của bạn.";
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
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">CÔng việc của bạn đã có người ứng tuyển.</h1>
					<p style=""margin: 0; margin-top: 20px;"">" + text + @"</p>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendMailAppySuccessfull(string gmail, string jobid, string jobName)
        {
            try
            {
                string text = "Bạn đã ứng tuyển công việc  " + jobName + " thành công. Xin vui lòng truy cập vào trang web để biết thêm thông tin công việc."; ;
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
                email.To.Add(MailboxAddress.Parse(gmail));
                email.Subject = "[Thông báo] - Bạn đẫ .";
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
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Bạn đã ứng tuyển công việc thành công.</h1>
					<p style=""margin: 0; margin-top: 20px;"">" + text + @"</p>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendMailSetDone()
        {
            throw new NotImplementedException();
        }

        public void UpdateApplication(Application application)
        => ApplicationDAO.UpdateApplication(application);

        public void RejectAllApplicationPendingByJob(int jobId)
        => ApplicationDAO.RejectAllApplicationPendingByJob(jobId);

        public int getNumberSeekerApplyByJobId(int jobId)
        => ApplicationDAO.getNumberSeekerApplyByJobId(jobId);

        public int getNumberSeekerApplyByStatus(int jobId, int status)
        => ApplicationDAO.getNumberSeekerApplyByStatus(jobId, status);

        public PaginatedList<Application> GetReportsByCid(int cid, int pageIndex, int pageSize)
        => ApplicationDAO.GetReportsByCid(cid, pageIndex, pageSize);

        public int GetNumberFeedbackByCid(int cid)
        => ApplicationDAO.GetNumberFeedbackByCid(cid);

        public void DeleteAppication(Application application)
        => ApplicationDAO.DeletableApplication(application);

        public void SendMailCancelJobAssigner(string gmail, string jobName)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            email.To.Add(MailboxAddress.Parse(gmail));
            string text = "Công việc " + jobName + " đã bị hủy vì 1 lý do nào đó vui lòng liên hệ với nhà tuyển dụng để biết thêm thông tin chi tiết.";
            email.Subject = "[Thông báo] - Công việc của bạn đã bị hủy.";
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
                    <h1 style=""margin: 20px 0 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Công việc của bạn đã bị hủy.</h1>
					<p style=""margin: 0; margin-top: 20px;"">" + text + @"</p>
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
