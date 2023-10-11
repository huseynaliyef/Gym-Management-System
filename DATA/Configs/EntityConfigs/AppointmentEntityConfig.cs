using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configs.EntityConfigs
{
    public class AppointmentEntityConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(m => m.MemberId)
                .IsRequired();

            builder.Property(m => m.AppointmentDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.Duration)
                .IsRequired();
        }
    }
}
