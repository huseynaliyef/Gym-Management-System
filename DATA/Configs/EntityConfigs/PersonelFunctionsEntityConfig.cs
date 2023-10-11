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
    public class PersonelFunctionsEntityConfig : IEntityTypeConfiguration<PersonelFunctions>
    {
        public void Configure(EntityTypeBuilder<PersonelFunctions> builder)
        {
            builder.Property(m => m.FunctionName)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
