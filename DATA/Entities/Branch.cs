using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Branch:BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public string WorkDateFrom { get; set; }
        public string WorkDateTo { get; set; }
        public ICollection<Equipment>? Equipments { get; set; }
    }
}
