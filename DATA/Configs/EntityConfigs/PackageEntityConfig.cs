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
    public class PackageEntityConfig : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.Property(m => m.PackageName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Duration)
                .IsRequired();
                
        }
    }
}
