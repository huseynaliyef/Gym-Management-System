using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Member
{
    public class AppointmentAddDTO
    {
        public string? PersonelId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; }
    }
}
