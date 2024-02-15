using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ControllerDAO
{
    public class DashboardDAO
    {
        public static DashboardDTO GetStatisticsDaskboard(int year)
        {
            DashboardDTO dto = new DashboardDTO();
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    dto.NumberJobAssigner = context.Customers.Where(x => x.Role == 1 && x.CreateDate.Year == year).Count();
                    dto.NumberJobSeeker = context.Customers.Where(x => x.Role == 2 && x.CreateDate.Year == year).Count();
                    dto.NumberJobCategory = context.JobsCategories.Count();
                    dto.NumberSkill = context.Skills.Count();
                    dto.NumberSkillCategory = context.SkillCategories.Count();
                    dto.NumberStaff = context.Users.Where(x => x.Role == 1).Count();
                    dto.NumberJobWaitingApprover = context.Jobs.Where(x => x.Status == 0 && x.StartDate.Year == year).Count();
                    dto.NumberJobApprover = context.Jobs.Where(x => x.Status == 1 && x.StartDate.Year == year).Count();
                    dto.NumberJobWaitingEdit = context.Jobs.Where(x => x.Status == 2 && x.StartDate.Year == year).Count();
                    dto.NumberJobReject = context.Jobs.Where(x => x.Status == 3 && x.StartDate.Year == year).Count();
                    dto.NumberJobCompete = context.Jobs.Where(x => x.Status == 4 && x.StartDate.Year == year).Count();
                    dto.NumberEpointDeposit = context.Transactions.Where(x => x.TransactionType == 1 && x.Paymentdate.Year == year).Sum(x => x.Epoint ?? 0);
                    dto.NumberEpointWithDraw = context.Transactions.Where(x => x.TransactionType == 2 && x.Paymentdate.Year == year).Sum(x => x.Epoint ?? 0);
                    dto.NumberEpointCreatePost = context.Transactions.Where(x => (x.TransactionType == 3 || x.TransactionType == 4) && x.Paymentdate.Year == year).Sum(x => x.Epoint ?? 0);
                    dto.NumberWaittingApplyJobPerson = context.Applications.Where(x => x.Status == 0 && x.StartDate.Year == year).Count();
                    dto.NumberApplyJobPerson = context.Applications.Where(x => x.Status == 1 && x.StartDate.Year == year).Count();
                    dto.NumberCompleteJobPerson = context.Applications.Where(x => x.Status == 2 && x.StartDate.Year == year).Count();
                    dto.NumberRejectApplyJobPerson = context.Applications.Where(x => x.Status == 3 && x.StartDate.Year == year).Count();
                    dto.NumberJobOnline = context.Jobs.Where(x => x.TypeJobs.Equals("ONLINE") && x.StartDate.Year == year).Count();
                    dto.NumberJobOffline = context.Jobs.Where(x => x.TypeJobs.Equals("OFFLINE") && x.StartDate.Year == year).Count();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dto;
        }

        public static List<ChartDTO> GetChart(int year)
        {
            List<ChartDTO> chartDTOs = new List<ChartDTO>();
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        int numberJobAssgner = context.Customers
                            .Where(c => c.Role == 1 && c.Status == 1 && (c.CreateDate.Year) == year && c.CreateDate.Month == month)
                            .Count();

                        int numberJobSeeker = context.Customers
                            .Where(c => c.Role == 2 && c.Status == 1 && c.CreateDate.Year == year && c.CreateDate.Month == month)
                            .Count();

                        chartDTOs.Add(new ChartDTO
                        {
                            Month = month,
                            NumberJobAssgner = numberJobAssgner,
                            NumberJobSeeker = numberJobSeeker
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return chartDTOs;
        }
    }
}
