using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Equipment:BaseEntity
    {
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
        public string? EquipmentName { get; set; }
        public string? Description { get; set; }
        public int count { get; set; }
        public bool Status { get; set; }
    }
}
