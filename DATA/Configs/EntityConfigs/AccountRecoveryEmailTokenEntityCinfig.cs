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
    public class AccountRecoveryEmailTokenEntityCinfig : IEntityTypeConfiguration<AccountRecoveryEmailToken>
    {
        public void Configure(EntityTypeBuilder<AccountRecoveryEmailToken> builder)
        {
            builder.Property(m => m.Token)
                .HasMaxLength(400);
        }
    }
}
