using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessObject.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Uid);
            //builder.HasNoKey();
            //builder.HasKey(x => x.Cid);
            //builder.Property(x => x.ClubCode).HasMaxLength(50);
            //builder.Property(x => x.ClubName).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.ClubDesc).IsRequired(false).HasMaxLength(2000);
            //builder.Property(x => x.ClubEmail).IsRequired().HasMaxLength(128);
            //builder.Property(x => x.ClubBirthday).IsRequired(false);

            //config trong bảng nhiều 
            #region ConfigRelationships
            builder.HasMany(x => x.Reports).WithOne(x => x.User).HasForeignKey(x => x.Uid);
            builder.HasMany(x => x.TypeManagerUsers).WithOne(x => x.User).HasForeignKey(x => x.Uid);
            #endregion
        }
    }
}
