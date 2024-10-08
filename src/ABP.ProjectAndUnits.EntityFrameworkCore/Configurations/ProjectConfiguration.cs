using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ABP.ProjectAndUnits.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ConfigureByConvention();
            builder.ToTable("Projects");
            builder.HasKey(c => c.Id);


            builder.Property(c => c.ProjectCode)
                .HasMaxLength(5);

            builder.Property(c => c.Name)
                .HasMaxLength(ProjectAndUnitsConsts.GeneralTextStringLenght);

            builder.Property(c => c.Descrption)
                .HasMaxLength(ProjectAndUnitsConsts.GeneralDesriptionStringLenght);

            builder.Property(c => c.ProjectLocation)
                .HasMaxLength(ProjectAndUnitsConsts.GeneralTextStringLenght);

            builder.Property(c => c.NumberOfUnits)
            .IsRequired()
            .HasColumnType("int")
            .HasDefaultValue(1);

            builder.HasMany(e => e.Units)
               .WithOne()
               .HasForeignKey(e => e.ProjectId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
