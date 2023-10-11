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
    public class BranchEntityConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(90);

            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(m => m.WorkDateFrom)
                .IsRequired();

            builder.Property(m => m.WorkDateTo)
                .IsRequired();

        }
    }
}
