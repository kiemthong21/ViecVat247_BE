using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessObject.Configurations
{
    public class TypeManagerConfiguration : IEntityTypeConfiguration<TypeManager>
    {
        public void Configure(EntityTypeBuilder<TypeManager> builder)
        {
            builder.ToTable(nameof(TypeManager));
            builder.HasKey(x => x.TypeManagerId);
            //builder.Property(x => x.ClubCode).HasMaxLength(50);
            //builder.Property(x => x.ClubName).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.ClubDesc).IsRequired(false).HasMaxLength(2000);
            //builder.Property(x => x.ClubEmail).IsRequired().HasMaxLength(128);
            //builder.Property(x => x.ClubBirthday).IsRequired(false);

            //config trong bảng nhiều 
            #region ConfigRelationships
            //builder.HasMany(x => x.Comments).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
            #endregion
        }
    }
}
