using BussinessObject.Configurations;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.Viecvat247Context
{
    public class Viecvat247DBcontext : DbContext
    {
        public Viecvat247DBcontext(DbContextOptions options) : base(options)
        {

        }

        public Viecvat247DBcontext()
        {
        }


        /// <summary>
        /// Get connection strings in appsetings.json
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("viecvat247DB"));
            }
        }
        /// <summary>
        /// Apply db configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerSkillConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerRoomConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new JobsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new JobsSkillConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportImageConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new SkillCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReasonForEditingJobStatusConfiguration());
            modelBuilder.ApplyConfiguration(new TypeManagerConfiguration());
            modelBuilder.ApplyConfiguration(new TypeManagerUserConfiguration());

            base.OnModelCreating(modelBuilder);

        }

        #region DbSet
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerSkill> CustomerSkills { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobsCategory> JobsCategories { get; set; } = null!;
        public virtual DbSet<JobsSkill> JobsSkills { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportImage> ReportImages { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<SkillCategory> SkillCategories { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Room> Room { get; set; } = null!;

        public virtual DbSet<CustomerRoom> CustomerRoom { get; set; } = null!;
        public virtual DbSet<ReasonForEditingJobStatus> ReasonForEditingJobStatus { get; set; } = null!;
        public virtual DbSet<TypeManager> TypeManager { get; set; } = null!;
        public virtual DbSet<TypeManagerUser> TypeManagerUser { get; set; } = null!;


        #endregion
    }
}
