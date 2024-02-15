using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessObject.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasKey(x => x.Cid);
            //builder.Property(x => x.ClubCode).HasMaxLength(50);
            //builder.Property(x => x.ClubName).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.ClubDesc).IsRequired(false).HasMaxLength(2000);
            //builder.Property(x => x.ClubEmail).IsRequired().HasMaxLength(128);
            //builder.Property(x => x.ClubBirthday).IsRequired(false);

            //config trong bảng nhiều 
            #region ConfigRelationships
            builder.HasMany(x => x.Reports).WithOne(x => x.Customer).HasForeignKey(x => x.Cid);
            builder.HasMany(x => x.MessageSenders).WithOne(x => x.Sender).HasForeignKey(x => x.SenderId);
            builder.HasMany(x => x.Notifications).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Applications).WithOne(x => x.Applicant).HasForeignKey(x => x.ApplicantId);
            builder.HasMany(x => x.CustomerSkills).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.Transactions).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.Jobs).WithOne(x => x.JobAssigner).HasForeignKey(x => x.JobAssignerId);





            #endregion
        }
    }
}
