using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Member:IdentityUser
    {
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Photo { get; set; }
        public string? RefreshToken { get; set; }
        
        public ICollection<PersonelFunctions>? Functions { get; set; }
        public ICollection<MemberMembership>? MemberMemberships { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<TeacherNotesToStudent>? TeacherNotesToStudents { get; set;}
    }
}
