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
    public class PaymentEntityConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(m => m.MemberId)
                .IsRequired();

            builder.Property(m=>m.Amount)
                .IsRequired();

            builder.Property(m => m.PaymentDate)
                .IsRequired();

            builder.Property(m => m.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
