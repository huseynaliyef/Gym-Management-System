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
    public class EquipmentEntityConfig : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.Property(m => m.BranchId)
                .IsRequired();

            builder.Property(m => m.EquipmentName)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(m => m.Description)
                .HasMaxLength(400);
        }
    }
}
