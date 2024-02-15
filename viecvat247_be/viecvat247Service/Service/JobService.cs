using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using static System.Net.Mime.MediaTypeNames;

namespace viecvat247Service.Service
{
    public class JobService : IJobService
    {

        IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();


        public JobService() { }

        public Job CreateJob(Job jobCreate)
            => JobDAO.CreateJob(jobCreate);

        public PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string typesJobs)
            => JobDAO.GetAllJob(cid, searchValue, pageIndex, pageSize, orderBy,typesJobs);

        public PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string orderBy)
            => JobDAO.GetAllJobStaff(cid, searchValue, pageIndex, pageSize, orderBy);
        public PaginatedList<Job> GetAllJobStaff(string cid, string searchValue, int pageIndex, int pageSize, string typeJob, string orderBy, string status)
            => JobDAO.GetAllJobStaff(cid, searchValue, pageIndex, pageSize, typeJob, orderBy,status);
        public PaginatedList<Job> GetAllJob(string cid, string searchValue, int pageIndex, int pageSize, string orderBy, int customerId, string typesJobs, string status)
        => JobDAO.GetAllJob(cid, searchValue, pageIndex, pageSize, orderBy,customerId, typesJobs, status);

        public PaginatedList<Job> GetAllJob(string uid, string cid, string searchValue, int pageIndex, int pageSize, string orderBy, string status)
        => JobDAO.GetAllJob(uid, cid, searchValue, pageIndex, pageSize, orderBy, status);

        public Job getJobById(int jid)
            => JobDAO.GetJobById(jid);

        public Job GetJob(int id)
           => JobDAO.GetJobById(id);

        public JobService(IConfiguration config)
        {
            this.config = config;
        }
        public IConfiguration Configuration { get; }
        public void SendMail(string email, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(config.GetSection("Constants:ClientID").Value));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = "[Thông báo] " + subject;
            mail.Body = new TextPart(TextFormat.Html)
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
					<p style=""margin: 0; margin-top: 20px;"">" + message + @"</p>
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
" };
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(config.GetSection("Constants:Host").Value,
                    Int32.Parse(config.GetSection("Constants:Port").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(config.GetSection("Constants:ClientID").Value,
                    config.GetSection("Constants:ClientSecret").Value);
                smtp.Send(mail);
                smtp.Disconnect(true);
            };
        }
        public void UpdateJob(Job job)
            => JobDAO.UpdateJob(job);

        public void CreateJobSkill(string[]? listSkill, int jobsId)
        => JobDAO.CreateJobSkill(listSkill, jobsId);

        public void DeleteJobSkillbyJobId(int jobsId)
        => JobDAO.DeleteJobSkillbyJobId(jobsId);

        public void CensorshipJob(Job job, int uid, CensorshipDTO cen)
        {
            JobDAO.CensorshipJob(job, uid, cen);
        }
    }
}
