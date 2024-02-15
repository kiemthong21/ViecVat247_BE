using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessObject.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable(nameof(Report));
            builder.HasKey(x => x.ReportId);
            //builder.HasNoKey();
            //builder.HasKey(x => x.Cid);
            //builder.Property(x => x.ClubCode).HasMaxLength(50);
            //builder.Property(x => x.ClubName).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.ClubDesc).IsRequired(false).HasMaxLength(2000);
            //builder.Property(x => x.ClubEmail).IsRequired().HasMaxLength(128);
            //builder.Property(x => x.ClubBirthday).IsRequired(false);

            //config trong bảng nhiều 
            #region ConfigRelationships
            builder.HasMany(x => x.ReportImages).WithOne(x => x.Report).HasForeignKey(x => x.ReportId);
            #endregion
        }
    }
}
