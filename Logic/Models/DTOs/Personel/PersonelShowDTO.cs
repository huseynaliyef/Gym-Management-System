using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Personel
{
    public class PersonelShowDTO
    {
        public string? Id { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<PersonelAddFunctionDTO>? Functions { get; set; }
    }
}
