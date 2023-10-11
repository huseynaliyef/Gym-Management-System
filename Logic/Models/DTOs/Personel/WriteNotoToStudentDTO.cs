using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Personel
{
    public class WriteNotoToStudentDTO
    {
        public string? MemberId { get; set; }
        public string? Note { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
