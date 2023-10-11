using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Appointment:BaseEntity
    {
        [ForeignKey("Member")]
        public string? MemberId { get; set; }
        public Member? Member { get; set; }
        [ForeignKey("Personel")]
        public string? PersonelId { get; set; }
        public Member? Personel { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; }
    }
}
