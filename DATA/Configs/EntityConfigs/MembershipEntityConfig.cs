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
    public class MembershipEntityConfig : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(m => m.MembershipName).IsRequired().HasMaxLength(30);
            builder.Property(m => m.PackageId).IsRequired();
        }
    }
}
