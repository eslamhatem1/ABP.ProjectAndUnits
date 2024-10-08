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
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable("Units");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descrption)
           .HasMaxLength(ProjectAndUnitsConsts.GeneralDesriptionStringLenght);

            builder.Property(c => c.Location)
                .HasMaxLength(ProjectAndUnitsConsts.GeneralTextStringLenght);

            builder.Property(c => c.UnitArea)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(c => c.NumberOfRooms)
            .IsRequired()
            .HasColumnType("int");

        }
    }
}
