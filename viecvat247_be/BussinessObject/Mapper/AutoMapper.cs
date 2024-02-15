using AutoMapper;
using BussinessObject.DTO;
using BussinessObject.Models;

namespace BussinessObject.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Job, JobDetailDTO>()
                .ForMember(o => o.JobAssignerName, opt => opt.MapFrom(src => src.JobAssigner!.FullName))
                .ForMember(o => o.JobCategoryName, opt => opt.MapFrom(src => src.JobCategory!.JobCategoryName));
            CreateMap<JobCreateDTO, Job>();

            CreateMap<JobUpdateDTO, Job>();

            CreateMap<CustomerEditProfileDTO, Customer>();

            CreateMap<NewSkillDTO, Skill>();

            CreateMap<SkillDTO, Skill>();

            CreateMap<Customer, CustomerProfileDTO>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.CustomerSkills.Select(cs => cs.Skill)));

            CreateMap<User, AccountLoginDTO>()
                .ForMember(dest => dest.TypeManagers, opt => opt.MapFrom(src => src.TypeManagerUsers.Select(cs => cs.TypeManager)));

            CreateMap<Customer, CustomerProfileGetJobDTO>()
                .ForMember(dest => dest.numberJobs, opt => opt.MapFrom(src =>
                        src.Jobs.Count(j => j.Status.HasValue && new[] {1,4}.Contains(j.Status.Value))))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.CustomerSkills.Select(cs => cs.Skill)));
            
            CreateMap<CustomerProfileGetJobDTO, Customer>();

            CreateMap<Skill, SkillDTO>()
                .ForMember(o => o.SkillCategoryName, otp => otp.MapFrom(src => src.SkillCategory!.SkillCategoryName));

            CreateMap<TypeManager, TypeManagerDTO>();

            CreateMap<SkillCategory, SkillCategoryDTO>();

            CreateMap<NewSkillCategoryDTO, SkillCategory>();

            CreateMap<NewCategoryJobDTO, JobsCategory>();

            CreateMap<JobsCategory, JobsCategoryDTO>();

            CreateMap<NewStaffDTO, User>();
            CreateMap<User, StaffDTO>()
               .ForMember(dest => dest.TypeManagers, opt => opt.MapFrom(src => src.TypeManagerUsers.Select(cs => cs.TypeManager)));

            CreateMap<CustomerProfileDTO, Customer>();

            CreateMap<Application, ApplicationJobDTO>()
                .ForMember(o => o.JobsId, opt => opt.MapFrom(src => src.Job!.JobsId))
                .ForMember(o => o.JobAssignerId, opt => opt.MapFrom(src => src.Job.JobAssignerId))
                .ForMember(o => o.JobCategoryId, opt => opt.MapFrom(src => src.Job.JobCategoryId))
                .ForMember(o => o.JobCategory, opt => opt.MapFrom(src => src.Job!.JobCategory!.JobCategoryName))
                .ForMember(o => o.Title, opt => opt.MapFrom(src => src.Job!.Title))
                .ForMember(o => o.Image, opt => opt.MapFrom(src => src.Job!.Image))
                .ForMember(o => o.Job_Overview, opt => opt.MapFrom(src => src.Job!.Job_Overview))
                .ForMember(o => o.Required_Skills, opt => opt.MapFrom(src => src.Job!.Required_Skills))
                .ForMember(o => o.Preferred_Skills, opt => opt.MapFrom(src => src.Job!.Preferred_Skills))
                .ForMember(o => o.TypeJobs, opt => opt.MapFrom(src => src.Job!.TypeJobs))
                .ForMember(o => o.Location, opt => opt.MapFrom(src => src.Job!.Location))
                .ForMember(o => o.Address, opt => opt.MapFrom(src => src.Job!.Address))
                .ForMember(o => o.WorkingTime, opt => opt.MapFrom(src => src.Job!.WorkingTime))
                .ForMember(o => o.NumberPerson, opt => opt.MapFrom(src => src.Job!.NumberPerson))
                .ForMember(o => o.JobStatus, opt => opt.MapFrom(src => src.Job!.Status))
                .ForMember(o => o.JobAssignerEmail, opt => opt.MapFrom(src => src.Job!.JobAssigner.Cemail))
                .ForMember(o => o.ApplyStatus, opt => opt.MapFrom(src => src.Status));

            CreateMap<Customer, CustomerDTO>();

            CreateMap<Customer, CustomerCensorshipDTO>()
                .ForMember(o => o.Aid, opt => opt.MapFrom(src => src.Applications.FirstOrDefault().Aid));

            CreateMap<NewApplyRequestDTO, Application>();

            CreateMap<Application, CustomerApplyDTO>()
                .ForMember(o => o.Cemail, opt => opt.MapFrom(src => src.Applicant.Cemail))
                .ForMember(o => o.PhoneNumber, opt => opt.MapFrom(src => src.Applicant.PhoneNumber))
                .ForMember(o => o.FullName, opt => opt.MapFrom(src => src.Applicant.FullName))
                .ForMember(o => o.Location, opt => opt.MapFrom(src => src.Applicant.Location))
                .ForMember(o => o.Address, opt => opt.MapFrom(src => src.Applicant.Address))
                .ForMember(o => o.Descrition, opt => opt.MapFrom(src => src.Applicant.Descrition))
                .ForMember(o => o.Avatar, opt => opt.MapFrom(src => src.Applicant.Avatar))
                .ForMember(o => o.Dob, opt => opt.MapFrom(src => src.Applicant.Dob))
                .ForMember(o => o.CreateDate, opt => opt.MapFrom(src => src.Applicant.CreateDate))
                .ForMember(o => o.Gender, opt => opt.MapFrom(src => src.Applicant.Gender))
                .ForMember(o => o.CV, opt => opt.MapFrom(src => src.Applicant.CV))
                .ForMember(o => o.Voting, opt => opt.MapFrom(src => src.Applicant.Voting));

            CreateMap<ReportImage, ReportImageDTO>();

            CreateMap<Report, ReportDTO>()
                .ForMember(o => o.CustomerName, opt => opt.MapFrom(src => src!.Customer!.FullName))
                .ForMember(o => o.CustomerEmail, opt => opt.MapFrom(src => src!.Customer!.Cemail))
                .ForMember(o => o.EmployeeName, opt => opt.MapFrom(src => src!.User!.FullName));
            CreateMap<Transaction, DepositeDTO>();
            CreateMap<DepositeDTO, Transaction>();
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(o => o.CustomerName, opt => opt.MapFrom(src => src.Customer!.FullName))
                .ForMember(o => o.ReceiverName, opt => opt.MapFrom(src => src.Receiver!.FullName))
                .ForMember(o => o.JobName, opt => opt.MapFrom(src => src.Job!.Title));

            CreateMap<Notification, NotificationDTO>();
            CreateMap<Customer, FeedbackDTO>();
            CreateMap<Application, FeedbackDTO>()
                .ForMember(o => o.JobName, opt => opt.MapFrom(src => src.Job!.Title))
                .ForMember(o => o.JobAssignerId, opt => opt.MapFrom(src => src.Job!.JobAssignerId))
                .ForMember(o => o.SkillName, opt => opt.MapFrom(src => src.Job!.JobsSkills.Select(js => js.Skill.SkillName)))
                .ForMember(o => o.JobAssignerName, opt => opt.MapFrom(src => src.Job!.JobAssigner.FullName))
                .IncludeMembers(src => src.Job!.JobAssigner);
            
        }
    }
}
