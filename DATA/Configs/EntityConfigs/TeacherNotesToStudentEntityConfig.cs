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
    public class TeacherNotesToStudentEntityConfig : IEntityTypeConfiguration<TeacherNotesToStudent>
    {
        public void Configure(EntityTypeBuilder<TeacherNotesToStudent> builder)
        {
            builder.Property(m => m.NoteDate).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
