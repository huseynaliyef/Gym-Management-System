using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class TeacherNotesToStudent:BaseEntity
    {
        public string? PersonelId { get; set; }
        public Member? Personel { get; set; }
        public string? MemberId { get; set; }
        public Member? Member { get; set; }

        public string? Note { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
